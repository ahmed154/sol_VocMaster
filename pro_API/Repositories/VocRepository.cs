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
using Microsoft.Extensions.Configuration.UserSecrets;

namespace pro_API.Repositories
{
    public class VocRepository : IVocRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public VocRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<VocVM>> IVocRepository.Search(string name)
        {
            List<VocVM> vocVMs = new List<VocVM>();

            IQueryable<Voc> query = appDbContext.Vocs;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Text.Contains(name));
            }

            var vocs = await query.ToListAsync();

            foreach (var voc in vocs)
            {
                vocVMs.Add(new VocVM { Voc = voc });
            }
            return vocVMs;
        }
        public async Task<List<VocVM>> GetVocs()
        {
            List<VocVM> vocVMs = new List<VocVM>();
            var vocs = await appDbContext.Vocs.ToListAsync();
            foreach (var voc in vocs)
            {
                vocVMs.Add(new VocVM { Voc = voc});
            }
            return vocVMs; 
        }
        public async Task<VocVM> GetVocCheck(string userId)
        {
            VocVM vocVM = new VocVM();

            List<UserVoc> userVoc = await appDbContext.UserVocs
                .Where(x => x.UserId == userId).ToListAsync();

            userVoc = userVoc.Where(x => x.NextReviewTime < DateTime.UtcNow).ToList();

            var x = userVoc
                .OrderByDescending(x => x.NextReviewTime)
                .First();

            if(userVoc != null)
            {
                vocVM.Voc = await appDbContext.Vocs.FirstOrDefaultAsync(e => e.Id == x.VocId);
            }
            
            return vocVM;
        }
        public async Task<VocVM> GetVoc(int id)
        {
            VocVM vocVM = new VocVM();
            vocVM.Voc = await appDbContext.Vocs.FirstOrDefaultAsync(e => e.Id == id);
            return vocVM;
        }
        public async Task<VocVM> CreateVoc(VocVM vocVM)
        {
            var result = await appDbContext.Vocs.AddAsync(vocVM.Voc);
            await appDbContext.SaveChangesAsync();

            vocVM.Voc = result.Entity;
            return vocVM;
        }
        public async Task<VocVM> UpdateVoc(VocVM vocVM)
        {
            Voc result = await appDbContext.Vocs
                .FirstOrDefaultAsync(e => e.Id == vocVM.Voc.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(vocVM.Voc, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new VocVM { Voc = result };
            }

            return null;
        }
        public async Task<VocVM> DeleteVoc(int vocId)
        {
            var result = await appDbContext.Vocs
                .FirstOrDefaultAsync(e => e.Id == vocId);
            if (result != null)
            {
                appDbContext.Vocs.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new VocVM { Voc = result };
            }

            return null;
        }
        /// <summary>
        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        public async Task<Voc> GetVocByname(Voc voc)
        {
            return await appDbContext.Vocs.Where(n => n.Text == voc.Text && n.Id != voc.Id)
                .FirstOrDefaultAsync();
        }


        public async Task<List<VocCardVM>> GetControlVocCardVMs(VocCardVM vocCardVM)
        {
            List<VocCardVM> vocCardVMs = new List<VocCardVM>();
            List<Voc> Vocs = appDbContext.Vocs.Where(x => x.Id > 100 && x.Id <= 120).ToList();
          
            foreach (var voc in Vocs)
            {
                VocCardVM vocCardVM1 = new VocCardVM();

                vocCardVM1.Voc = voc;
                vocCardVM1.VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == voc.Id).ToListAsync();
                vocCardVM1.Definitions = await appDbContext.Definitions.Where(x => x.VocId == voc.Id).Include(e => e.Examples).ToListAsync();
                vocCardVM1.Images = await appDbContext.Images.Where(x => x.VocId == voc.Id).ToListAsync();
                vocCardVM1.Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == voc.Id).ToListAsync();
                vocCardVM1.Translates = await appDbContext.Translates.Where(x => x.VocId == voc.Id).ToListAsync();

                vocCardVMs.Add(vocCardVM1);

                //vocCardVMs.Add(new VocCardVM
                //{
                //    Voc = voc,
                //    VocAudios = await appDbContext.VocAudios.Where(x => x.VocId == voc.Id).ToListAsync(),
                //    Definitions = await appDbContext.Definitions.Where(x => x.VocId == voc.Id).Include(e => e.Examples).ToListAsync(),
                //    Images = await appDbContext.Images.Where(x => x.VocId == voc.Id).ToListAsync(),
                //    Synonyms = await appDbContext.Synonyms.Where(x => x.VocId == voc.Id).ToListAsync(),
                //    Translates = await appDbContext.Translates.Where(x => x.VocId == voc.Id).ToListAsync()
                //});
            }

            return vocCardVMs;
        }
    }
}
