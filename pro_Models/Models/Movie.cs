using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string MovieId { get; set; }
        [Required]
        public string Text { get; set; }
        public int LangId { get; set; } = 1;
        ///////////////////////////////////////////////////////////////////
        public List<Subtitle> Subtitles { get; set; }
    }
}
