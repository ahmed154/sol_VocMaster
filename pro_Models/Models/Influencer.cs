using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Influencer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        ////////////////////////////////////////////////////////////
        public List<Quote> Quotes { get; set; }
    }
}
