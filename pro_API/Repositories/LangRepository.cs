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

namespace pro_API.Repositories
{
    public class LangRepository : ILangRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public LangRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<LangVM>> ILangRepository.Search(string name)
        {
            List<LangVM> langVMs = new List<LangVM>();

            IQueryable<Lang> query = appDbContext.Langs;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var langs = await query.ToListAsync();

            foreach (var lang in langs)
            {
                langVMs.Add(new LangVM { Lang = lang });
            }
            return langVMs;
        }
        public async Task<List<LangVM>> GetLangs()
        {
            List<LangVM> langVMs = new List<LangVM>();
            var langs = await appDbContext.Langs.ToListAsync();
            foreach (var lang in langs)
            {
                langVMs.Add(new LangVM { Lang = lang});
            }
            return langVMs; 
        }
        public async Task<LangVM> GetLang(int id)
        {
            LangVM langVM = new LangVM();
            langVM.Lang = await appDbContext.Langs.FirstOrDefaultAsync(e => e.Id == id);
            return langVM;
        }
        public async Task<LangVM> CreateLang(LangVM langVM)
        {
            var result = await appDbContext.Langs.AddAsync(langVM.Lang);
            await appDbContext.SaveChangesAsync();

            langVM.Lang = result.Entity;
            return langVM;
        }
        public async Task<LangVM> UpdateLang(LangVM langVM)
        {
            Lang result = await appDbContext.Langs
                .FirstOrDefaultAsync(e => e.Id == langVM.Lang.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(langVM.Lang, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new LangVM { Lang = result };
            }

            return null;
        }
        public async Task<LangVM> DeleteLang(int langId)
        {
            var result = await appDbContext.Langs
                .FirstOrDefaultAsync(e => e.Id == langId);
            if (result != null)
            {
                appDbContext.Langs.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new LangVM { Lang = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Lang> GetLangByname(Lang lang)
        {
            return await appDbContext.Langs.Where(n => n.Name == lang.Name && n.Id != lang.Id)
                .FirstOrDefaultAsync();
        }
    }
}
