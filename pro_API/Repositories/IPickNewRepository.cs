using pro_Models.Models;
using pro_Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace pro_API.Repositories
{
    public interface IPickNewRepository
    {
        Task<PickNewVM> GetPickNew(PickNewVM pickNewVM);
        Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM);
    }
}
