using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace pro_Models.ViewModels
{
    public class UserVocListVM
    {
        public List<UserVoc> UserVocs { get; set; } = new List<UserVoc>();
        public int LastVocId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Exception { get; set; }
    }
}
