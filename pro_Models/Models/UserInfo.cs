using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int LastVocId { get; set; }
        ////////////////////////////////////////////////////////////////////
        ///
        public User User { get; set; }
    }
}
