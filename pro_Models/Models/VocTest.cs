using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocTest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Freq { get; set; } = 1;
        public int SubtitleId { get; set; }
        public int MovieId { get; set; }
        ////////////////////////////////////////////////////////
        public Subtitle Subtitle { get; set; }
        public Movie Movie { get; set; }
    }
}
