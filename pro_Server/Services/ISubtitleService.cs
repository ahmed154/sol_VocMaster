using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface ISubtitleService
    {
        Task<List<SubtitleVM>> GetSubtitles();
        Task<SubtitleVM> GetSubtitle(int id);
        Task<SubtitleVM> UpdateSubtitle(int id, SubtitleVM subtitleVM);
        Task<SubtitleVM> CreateSubtitle(SubtitleVM subtitleVM);
        Task<SubtitleVM> DeleteSubtitle(int id);
        Task<List<SubtitleVM>> CalcSubtitlesVM(Voc voc);
    }
}
