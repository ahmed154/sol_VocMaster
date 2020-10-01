using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IInfluencerRepository
    {
        Task<List<InfluencerVM>> Search(string name);
        Task<List<InfluencerVM>> GetInfluencers();
        Task<InfluencerVM> GetInfluencer(int influencerId);
        Task<InfluencerVM> CreateInfluencer(InfluencerVM influencerVM);
        Task<InfluencerVM> UpdateInfluencer(InfluencerVM influencer);
        Task<InfluencerVM> DeleteInfluencer(int influencerId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Influencer> GetInfluencerByName(string name);
        Task<Influencer> GetInfluencerByname(Influencer influencer);
    }
}
