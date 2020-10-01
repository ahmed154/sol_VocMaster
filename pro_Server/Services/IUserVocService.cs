using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IUserVocService
    {
        Task<List<UserVocVM>> GetUserVocs();
        Task<UserVocVM> GetUserVoc(int id);
        Task<UserVocVM> UpdateUserVoc(int id, UserVocVM uservocVM);
        Task<UserVocVM> CreateUserVoc(UserVocVM uservocVM);
        Task<UserVocVM> DeleteUserVoc(int id);
        Task<List<UserVocVM>> GetVocStudy(UserVocVM userVocVM);
        Task<List<UserVocVM>> GetVocCheck(UserVocVM userVocVM);
        Task UpdateUserVocList(UserVocListVM userVocVMsUpdate);
        Task CreateUserVocs(UserVocListVM userVocVMsCreate);
        Task UpdateUserVocsReview(UserVocListVM userVocVMsUpdate);
        Task<UserVocListVM> GetReviewVM(UserVocListVM reviewVM);
        Task<UserVocListVM> GetStudyVM(UserVocListVM studyVM);
        Task<VocCardVM> AddVocCardVM(VocCardVM vocCardVM);
        List<VocCardVM> GetVocCardVMs(UserNameVM userNameVM);
        Task<UserVocListVM> GetUserVocVMsResults(UserNameVM userNameVM);
    }
}
