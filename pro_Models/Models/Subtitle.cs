using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pro_Models.Models
{
    public class Subtitle
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public decimal StartTime { get; set; }
        [Required]
        public decimal EndtTime { get; set; }
        public int Rank { get; set; }
        public int MovieId { get; set; }
        //---------------------------------------------- relationships
        public Movie Movie { get; set; }
        public List<VocSubtitle> VocSubtitles { get; set; }
    }
}
