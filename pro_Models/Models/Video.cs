using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pro_Models.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int SubtitleId { get; set; }
        public int MovieId { get; set; }
        [NotMapped]
        public bool Visible { get; set; } = true;
        ///////////////////////////////////////////////////////////////////////////////////////
        public Subtitle Subtitle { get; set; }
        public Movie Movie { get; set; }
    }
}
