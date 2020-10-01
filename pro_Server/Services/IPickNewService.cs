using pro_Models.Models;
using pro_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IPickNewService
    {
        Task<PickNewVM> GetPickNew(PickNewVM pickNewVM);
        Task<List<VocCardVM>> GetVocCardVMs(UserNameVM userNameVM);
    }
}
