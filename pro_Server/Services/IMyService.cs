using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public interface IMyService
    {
        event Action RefreshRequested;
        event Action RefreshUserVocChech;
        void CallRequestRefresh();
        void CallRequestRefreshUserVocChech();
    }
}
