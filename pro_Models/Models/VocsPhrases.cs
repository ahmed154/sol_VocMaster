using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocsPhrases
    {
        public int Id { get; set; }
        public int VocId { get; set; }
        public int PhraseId { get; set; }
        /////////////////////////////////////////////////////////////////
        public Voc Voc { get; set; }
        public Phrase Phrase { get; set; }
    }
}
