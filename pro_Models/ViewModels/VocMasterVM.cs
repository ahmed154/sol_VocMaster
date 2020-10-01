using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.ViewModels
{
    public class VocMasterVM
    {
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public List<VocCardVM> News { get; set; } = new List<VocCardVM>();
        public List<VocCardVM> Studys { get; set; } = new List<VocCardVM>();
        public List<VocCardVM> Tests { get; set; } = new List<VocCardVM>();
        public List<UserVoc> Results { get; set; } = new List<UserVoc>();
        public string Exception { get; set; }
    }
}
