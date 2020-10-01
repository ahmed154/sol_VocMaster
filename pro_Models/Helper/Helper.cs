using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Helper
{
    public static class Helper
    {
        public static string CdnUri { get; set; }
        static Helper()
        {
            CdnUri  = @"https://VocMasterPullZone.b-cdn.net/";
        }

    }
}
