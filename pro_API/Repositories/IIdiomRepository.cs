using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IIdiomRepository
    {
        Task<List<IdiomVM>> Search(string name);
        Task<List<IdiomVM>> GetIdioms();
        Task<IdiomVM> GetIdiom(int idiomId);
        Task<IdiomVM> CreateIdiom(IdiomVM idiomVM);
        Task<IdiomVM> UpdateIdiom(IdiomVM idiom);
        Task<IdiomVM> DeleteIdiom(int idiomId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Idiom> GetIdiomByName(string name);
        Task<Idiom> GetIdiomByname(Idiom idiom);
    }
}
