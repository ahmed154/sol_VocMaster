using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IUserVocRepository
    {
        Task<List<UserVocVM>> Search(string name);
        Task<List<UserVocVM>> GetUserVocs();
        Task<UserVocVM> GetUserVoc(int uservocId);
        Task<UserVocVM> CreateUserVoc(UserVocVM uservocVM);
        Task<UserVocVM> UpdateUserVoc(UserVocVM uservoc);
        Task<UserVocVM> DeleteUserVoc(int uservocId);
        
        /////////////////////////////////////////////////////////// Other interface methods
        //Task<UserVoc> GetUserVocByName(string name);
        Task<UserVoc> GetUserVocByname(UserVoc uservoc);
        Task<List<UserVocVM>> GetVocStudy(string userId);
        Task<List<UserVocVM>> GetVocCheck(string userId);
        Task UpdateUserVocList(UserVocListVM userVocVMsUpdate);
        Task CreateUserVocs(UserVocListVM userVocVMsCreate);
        Task UpdateUserVocsReview(UserVocListVM userVocVMsUpdate);
        Task<UserVocListVM> GetReviewVM(UserVocListVM reviewVM);
        Task<UserVocListVM> GetStudyVM(UserVocListVM studyVM);
        Task<VocCardVM> AddVocCardVM(VocCardVM vocCardVM);
        Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM);
        Task<UserVocListVM> GetUserVocVMsResults(UserNameVM userNameVM);
    }
}
