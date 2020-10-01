using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ILangService
    {
        Task<List<LangVM>> GetLangs();
        Task<LangVM> GetLang(int id);
        Task<LangVM> UpdateLang(int id, LangVM langVM);
        Task<LangVM> CreateLang(LangVM langVM);
        Task<LangVM> DeleteLang(int id);
    }
}
