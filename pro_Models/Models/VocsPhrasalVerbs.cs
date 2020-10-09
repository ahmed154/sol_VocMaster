using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocsPhrasalVerbs
    {
        public int Id { get; set; }
        public int VocId { get; set; }
        public int PhrasalVerbId { get; set; }
        /////////////////////////////////////////////////////////////////
        public Voc Voc { get; set; }
        public PhrasalVerb PhrasalVerb { get; set; }
    }
}
