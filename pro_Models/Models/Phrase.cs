using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Phrase
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        ///////////////////////////////////////////////////////////////////////////////////
        public List<VocsPhrases> VocsPhrases { get; set; }

    }
}
