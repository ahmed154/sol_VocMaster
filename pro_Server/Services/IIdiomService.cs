using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IIdiomService
    {
        Task<List<IdiomVM>> GetIdioms();
        Task<IdiomVM> GetIdiom(int id);
        Task<IdiomVM> UpdateIdiom(int id, IdiomVM idiomVM);
        Task<IdiomVM> CreateIdiom(IdiomVM idiomVM);
        Task<IdiomVM> DeleteIdiom(int id);
    }
}
