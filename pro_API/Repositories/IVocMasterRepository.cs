using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IVocMasterRepository
    {
        Task<List<Image>> GetImagesByText(VocVM vocVM);
        Task<VocMasterVM> GetVocMasterVM(UserNameVM userNameVM);
        Task<List<VocSubtitle>> GetVocSubtitlesByText(VocVM vocVM);
        Task<VocVM> GetVocVMByText(VocVM vocVM);
        Task<VocMasterVM> UpdateVocMasterVM(VocMasterVM vocMasterVM);
    }
}
