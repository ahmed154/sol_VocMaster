using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IVocService
    {
        Task<List<VocVM>> GetVocs();
        Task<VocVM> GetVoc(int id);
        Task<VocVM> UpdateVoc(int id, VocVM vocVM);
        Task<VocVM> CreateVoc(VocVM vocVM);
        Task<VocVM> DeleteVoc(int id);
        Task<List<VocCardVM>> GetControlVocCardVMs(VocCardVM vocCardVM);
        Task<VocVM> GetVocVMByText(string txt);
    }
}
