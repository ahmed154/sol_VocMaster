using AutoMapper;
using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public SearchResultRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        #region Pro

        #endregion

        public async Task<List<Image>> GetImages(VocVM vocVM)
        {
            List<Image> images = new List<Image>();

            vocVM.Voc = await appDbContext.Vocs.FirstOrDefaultAsync(x => x.Text == vocVM.Voc.Text);
            if (vocVM.Voc != null) images = await appDbContext.Images.Where(x => x.VocId == vocVM.Voc.Id).ToListAsync();

            return images;
        }
        public async Task<List<Vid>> GetVids(VocVM vocVM)
        {
            List<VocSubtitle> vocSubtitles = new List<VocSubtitle>();
            List<Vid> vids = new List<Vid>();

            vocVM.Voc = appDbContext.Vocs.FirstOrDefault(x => x.Text == vocVM.Voc.Text);
            if (vocVM.Voc != null)
            {
                vocSubtitles = await appDbContext.VocsSubtitless.Where(x => x.VocId == vocVM.Voc.Id).Include(x => x.Subtitle).ThenInclude(x => x.Movie).ThenInclude(x => x.Subtitles).ToListAsync();

                foreach (var vocSubtitle in vocSubtitles)
                {
                    vids.Add(new Vid 
                    { 
                        YouTubeId = vocSubtitle.Subtitle.Movie.MovieId, 
                        Subs = vocSubtitle.Subtitle.Movie.Subtitles.Select(x => new Sub { StartTime = x.StartTime, EndTime = x.EndtTime, Text = x.Text}).ToList(), 
                        StartTime = vocSubtitle.Subtitle.StartTime
                    });                   
                }
            }

            return vids;
        }
    }
}
