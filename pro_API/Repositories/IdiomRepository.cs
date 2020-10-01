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
    public class IdiomRepository : IIdiomRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public IdiomRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<IdiomVM>> IIdiomRepository.Search(string name)
        {
            List<IdiomVM> idiomVMs = new List<IdiomVM>();

            IQueryable<Idiom> query = appDbContext.Idioms;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Text.Contains(name));
            }

            var idioms = await query.ToListAsync();

            foreach (var idiom in idioms)
            {
                idiomVMs.Add(new IdiomVM { Idiom = idiom });
            }
            return idiomVMs;
        }
        public async Task<List<IdiomVM>> GetIdioms()
        {
            List<IdiomVM> idiomVMs = new List<IdiomVM>();
            var idioms = await appDbContext.Idioms.ToListAsync();
            foreach (var idiom in idioms)
            {
                idiomVMs.Add(new IdiomVM { Idiom = idiom});
            }
            return idiomVMs; 
        }
        public async Task<IdiomVM> GetIdiom(int id)
        {
            IdiomVM idiomVM = new IdiomVM();
            idiomVM.Idiom = await appDbContext.Idioms.FirstOrDefaultAsync(e => e.Id == id);
            return idiomVM;
        }
        public async Task<IdiomVM> CreateIdiom(IdiomVM idiomVM)
        {
            var result = await appDbContext.Idioms.AddAsync(idiomVM.Idiom);
            await appDbContext.SaveChangesAsync();

            idiomVM.Idiom = result.Entity;
            return idiomVM;
        }
        public async Task<IdiomVM> UpdateIdiom(IdiomVM idiomVM)
        {
            Idiom result = await appDbContext.Idioms
                .FirstOrDefaultAsync(e => e.Id == idiomVM.Idiom.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(idiomVM.Idiom, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new IdiomVM { Idiom = result };
            }

            return null;
        }
        public async Task<IdiomVM> DeleteIdiom(int idiomId)
        {
            var result = await appDbContext.Idioms
                .FirstOrDefaultAsync(e => e.Id == idiomId);
            if (result != null)
            {
                appDbContext.Idioms.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new IdiomVM { Idiom = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Idiom> GetIdiomByname(Idiom idiom)
        {
            return await appDbContext.Idioms.Where(n => n.Text == idiom.Text && n.Id != idiom.Id)
                .FirstOrDefaultAsync();
        }
    }
}
