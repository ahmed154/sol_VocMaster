using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IInfluencerService
    {
        Task<List<InfluencerVM>> GetInfluencers();
        Task<InfluencerVM> GetInfluencer(int id);
        Task<InfluencerVM> UpdateInfluencer(int id, InfluencerVM influencerVM);
        Task<InfluencerVM> CreateInfluencer(InfluencerVM influencerVM);
        Task<InfluencerVM> DeleteInfluencer(int id);
    }
}
