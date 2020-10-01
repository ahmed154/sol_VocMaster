using Microsoft.EntityFrameworkCore;
using pro_API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using pro_API.Repositories;
using pro_Models.Models;
using pro_Models.ViewModels;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;

namespace pro_API.Repositories
{
    public class SubtitleRepository : ISubtitleRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public SubtitleRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }

        #region Pro
        #endregion
        async Task<List<SubtitleVM>> ISubtitleRepository.Search(string txt)
        {
            List<SubtitleVM> subtitleVMs = new List<SubtitleVM>();

            IQueryable<Subtitle> query = appDbContext.Subtitles;

            if (!string.IsNullOrEmpty(txt))
            {
                query = query.Where(e => e.Text.Contains(txt));
            }

            var subtitles = await query.ToListAsync();

            foreach (var subtitle in subtitles)
            {
                subtitleVMs.Add(new SubtitleVM { Subtitle = subtitle });
            }
            return subtitleVMs;
        }
        public async Task<List<SubtitleVM>> GetSubtitles()
        {
            List<SubtitleVM> subtitleVMs = new List<SubtitleVM>();
            var subtitles = await appDbContext.Subtitles.ToListAsync();
            foreach (var subtitle in subtitles)
            {
                subtitleVMs.Add(new SubtitleVM { Subtitle = subtitle});
            }
            return subtitleVMs; 
        }
        public async Task<SubtitleVM> GetSubtitle(int id)
        {
            SubtitleVM subtitleVM = new SubtitleVM();
            subtitleVM.Subtitle = await appDbContext.Subtitles.FirstOrDefaultAsync(e => e.Id == id);
            return subtitleVM;
        }
        public async Task<SubtitleVM> CreateSubtitle(SubtitleVM subtitleVM)
        {
            var result = await appDbContext.Subtitles.AddAsync(subtitleVM.Subtitle);
            await appDbContext.SaveChangesAsync();

            subtitleVM.Subtitle = result.Entity;
            return subtitleVM;
        }
        public async Task<SubtitleVM> UpdateSubtitle(SubtitleVM subtitleVM)
        {
            Subtitle result = await appDbContext.Subtitles
                .FirstOrDefaultAsync(e => e.Id == subtitleVM.Subtitle.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(subtitleVM.Subtitle, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new SubtitleVM { Subtitle = result };
            }

            return null;
        }
        public async Task<SubtitleVM> DeleteSubtitle(int subtitleId)
        {
            var result = await appDbContext.Subtitles
                .FirstOrDefaultAsync(e => e.Id == subtitleId);
            if (result != null)
            {
                appDbContext.Subtitles.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new SubtitleVM { Subtitle = result };
            }

            return null;
        }


        public async Task<List<SubtitleVM>> CalcSubtitlesVM(Voc voc)
        {
            List<Subtitle> subtitles = await appDbContext.Subtitles
                .Where(x => x.Text.Contains(" "+voc.Text+" "))
                .OrderBy(x => x.Rank)
                .ToListAsync();

            List<SubtitleVM> subtitlesVM = new List<SubtitleVM>();
            foreach (var subtitle in subtitles)
            {
                subtitle.Movie = await appDbContext.Movies.FirstOrDefaultAsync(x => x.Id == subtitle.MovieId);
                subtitle.Movie.Text = "";
                subtitle.Movie.Subtitles = null;

                subtitlesVM.Add(new SubtitleVM { Subtitle = subtitle});
            }

            return subtitlesVM;
        }
    }
}
