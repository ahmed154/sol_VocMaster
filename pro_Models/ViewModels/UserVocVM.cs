using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Text;

namespace pro_Models.ViewModels
{
    public class UserVocVM
    {
        [ValidateComplexType]
        public UserVoc UserVoc { get; set; } = new UserVoc();
        public VocVM VocVM => new VocVM { Voc = UserVoc.Voc }; 
        public string UserName { get; set; }
        public string Exception { get; set; }
    }
}
