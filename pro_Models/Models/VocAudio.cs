using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class VocAudio
    {
        public int Id { get; set; }
        [Required]
        public string Uri { get; set; }
        public string Phon { get; set; }
        public int VocId { get; set; }
        /////////////////////////////////////////////////////////
        public Voc Voc { get; set; } 
    }
}
