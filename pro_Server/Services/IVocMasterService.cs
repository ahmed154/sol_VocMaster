using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IVocMasterService
    {
        Task<VocVM> GetVocVMByText(VocVM vocVM);
        Task<VocMasterVM> GetVocMasterVM(UserNameVM userNameVM);
        Task<VocMasterVM> UpdateVocMasterVM(VocMasterVM vocMasterVM);
        Task<List<Image>> GetImagesByText(VocVM vocVM);
        Task<List<VocSubtitle>> GetVocSubtitlesByText(VocVM vocVM);
    }
}
