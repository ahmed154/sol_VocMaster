using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface ISearchResultRepository
    {
        Task<List<Image>> GetImages(VocVM vocVM);
        Task<List<Vid>> GetVids(VocVM vocVM);
    }
}
