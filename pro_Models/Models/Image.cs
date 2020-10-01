using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Uri { get; set; }
        public string Comment { get; set; }
        public int Like { get; set; } 
        public int Dislike { get; set; }
        public bool Liked { get; set; }
        public bool Disliked { get; set; }
        public int VocId { get; set; }
        //////////////////////////////////////////////////////////////////////////////// 
        public Voc Voc { get; set; }
    }
}
