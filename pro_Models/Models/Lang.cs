using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Lang
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Active { get; set; } 
    }
}
