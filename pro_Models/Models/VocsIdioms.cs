using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocsIdioms
    {
        public int Id { get; set; }
        public int VocId { get; set; }
        public int IdiomId { get; set; }
        /////////////////////////////////////////////////////////////////
        public Voc Voc { get; set; }
        public Idiom Idiom { get; set; }
    }
}
