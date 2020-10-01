using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ISubtitleRepository
    {
        Task<List<SubtitleVM>> Search(string name);
        Task<List<SubtitleVM>> GetSubtitles();
        Task<SubtitleVM> GetSubtitle(int subtitleId);
        Task<SubtitleVM> CreateSubtitle(SubtitleVM subtitleVM);
        Task<SubtitleVM> UpdateSubtitle(SubtitleVM subtitle);
        Task<SubtitleVM> DeleteSubtitle(int subtitleId);
        Task<List<SubtitleVM>> CalcSubtitlesVM(Voc voc);
    }
}
