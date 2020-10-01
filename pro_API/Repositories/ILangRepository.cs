using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ILangRepository
    {
        Task<List<LangVM>> Search(string name);
        Task<List<LangVM>> GetLangs();
        Task<LangVM> GetLang(int langId);
        Task<LangVM> CreateLang(LangVM langVM);
        Task<LangVM> UpdateLang(LangVM lang);
        Task<LangVM> DeleteLang(int langId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<Lang> GetLangByName(string name);
        Task<Lang> GetLangByname(Lang lang);
    }
}
