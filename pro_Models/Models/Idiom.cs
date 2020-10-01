using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class Idiom
    {
        public int Id { get; set; }
        public string Text { get; set; }
        /////////////////////////////////////////////////
        public List<VocsIdioms> VocsIdioms { get; set; }
    }
}
