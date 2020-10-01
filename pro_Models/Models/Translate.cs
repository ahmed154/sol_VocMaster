using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Translate
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int VocId { get; set; }
        public Voc Voc { get; set; }
    }
}
