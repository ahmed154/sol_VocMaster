using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.ViewModels
{
    public class SearchResultVM
    {
        public Voc Voc { get; set; }
        public List<Vid> Vids { get; set; }
        public List<Image> Images { get; set; }
        public string Exception { get; set; }
    }

    public class Vid
    {
        public string YouTubeId { get; set; }
        public List<Sub> Subs { get; set; }
        public decimal StartTime { get; set; }
    }
    public class Sub
    {
        public decimal StartTime { get; set; }
        public decimal EndTime { get; set; }
        public string Text { get; set; }

    }
}
