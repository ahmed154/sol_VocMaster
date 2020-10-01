using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Quote
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int InfluencerId { get; set; }
        ///////////////////////////////////////////////////////////////////////////
        public Influencer Influencer { get; set; }
        public List<VocsQuotes> VocsQuotes { get; set; }
    }
}
