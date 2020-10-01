using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IVocRepository
    {
        Task<List<VocVM>> Search(string name);
        Task<List<VocVM>> GetVocs();
        Task<VocVM> GetVoc(int vocId);
        Task<VocVM> CreateVoc(VocVM vocVM);
        Task<VocVM> UpdateVoc(VocVM voc);
        Task<VocVM> DeleteVoc(int vocId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Voc> GetVocByName(string name);
        Task<Voc> GetVocByname(Voc voc);
        Task<List<VocCardVM>> GetControlVocCardVMs(VocCardVM vocCardVM);
    }
}
