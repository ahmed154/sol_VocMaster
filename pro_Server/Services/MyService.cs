using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Server.Services
{
    public class MyService : IMyService
    {
        public event Action RefreshRequested;
        public event Action RefreshUserVocChech;

        public void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
        public void CallRequestRefreshUserVocChech()
        {
             RefreshUserVocChech?.Invoke();
        }
    }
}
