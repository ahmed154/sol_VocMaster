using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocSubtitle
    {
        public int Id { get; set; }
        public int VocId { get; set; }
        public int SubtitleId { get; set; }
        /////////////////////////////////////////////////////////
        public Voc Voc { get; set; }
        public Subtitle Subtitle { get; set; }
    }
}
