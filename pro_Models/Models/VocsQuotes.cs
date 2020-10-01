using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class VocsQuotes
    {
        public int Id { get; set; }
        public int VocId { get; set; }
        public int QuoteId { get; set; }
        /////////////////////////////////////////////////////////////////
        public Voc Voc { get; set; }
        public Quote Quote { get; set; }
    }
}
