using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.ViewModels
{
    public class Dif
    {
        public string Text { get; set; }
        public List<string> Examples { get; set; } = new List<string>();
        public string PageContent { get; set; }

    }
    public class CambridgeDif
    {
        public List<Dif> Difs { get; set; } = new List<Dif>();
        public string PageContent { get; set; }
    }
}
