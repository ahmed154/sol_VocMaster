﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pro_Models.Models
{
    public class User
    {
        public string Id { get; set; }
        [EmailAddress]
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Source { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
