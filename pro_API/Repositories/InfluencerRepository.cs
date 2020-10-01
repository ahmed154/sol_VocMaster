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
    public class InfluencerRepository : IInfluencerRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IMapper mapper;

        public InfluencerRepository(AppDbContext appDbContext, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.mapper = mapper;
        }
        async Task<List<InfluencerVM>> IInfluencerRepository.Search(string name)
        {
            List<InfluencerVM> influencerVMs = new List<InfluencerVM>();

            IQueryable<Influencer> query = appDbContext.Influencers;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(e => e.Name.Contains(name));
            }

            var influencers = await query.ToListAsync();

            foreach (var influencer in influencers)
            {
                influencerVMs.Add(new InfluencerVM { Influencer = influencer });
            }
            return influencerVMs;
        }
        public async Task<List<InfluencerVM>> GetInfluencers()
        {
            List<InfluencerVM> influencerVMs = new List<InfluencerVM>();
            var influencers = await appDbContext.Influencers.ToListAsync();
            foreach (var influencer in influencers)
            {
                influencerVMs.Add(new InfluencerVM { Influencer = influencer});
            }
            return influencerVMs; 
        }
        public async Task<InfluencerVM> GetInfluencer(int id)
        {
            InfluencerVM influencerVM = new InfluencerVM();
            influencerVM.Influencer = await appDbContext.Influencers.FirstOrDefaultAsync(e => e.Id == id);
            return influencerVM;
        }
        public async Task<InfluencerVM> CreateInfluencer(InfluencerVM influencerVM)
        {
            var result = await appDbContext.Influencers.AddAsync(influencerVM.Influencer);
            await appDbContext.SaveChangesAsync();

            influencerVM.Influencer = result.Entity;
            return influencerVM;
        }
        public async Task<InfluencerVM> UpdateInfluencer(InfluencerVM influencerVM)
        {
            Influencer result = await appDbContext.Influencers
                .FirstOrDefaultAsync(e => e.Id == influencerVM.Influencer.Id);

            if (result != null)
            {
                appDbContext.Entry(result).State = EntityState.Detached;
                result = mapper.Map(influencerVM.Influencer, result);
                appDbContext.Entry(result).State = EntityState.Modified;

                await appDbContext.SaveChangesAsync();
                return new InfluencerVM { Influencer = result };
            }

            return null;
        }
        public async Task<InfluencerVM> DeleteInfluencer(int influencerId)
        {
            var result = await appDbContext.Influencers
                .FirstOrDefaultAsync(e => e.Id == influencerId);
            if (result != null)
            {
                appDbContext.Influencers.Remove(result);
                await appDbContext.SaveChangesAsync();
                return new InfluencerVM { Influencer = result };
            }

            return null;
        }
/// <summary>
/// ///////////////////////////////////////////////////////////////////////////////////////////////////////////
/// </summary>
        public async Task<Influencer> GetInfluencerByname(Influencer influencer)
        {
            return await appDbContext.Influencers.Where(n => n.Name == influencer.Name && n.Id != influencer.Id)
                .FirstOrDefaultAsync();
        }
    }
}
