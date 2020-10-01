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
        public string Text { get; set; }
        public int ReleaseYear { get; set; }
        public string PosterUri { get; set; }
        public string TrailerUri { get; set; }
        public string MovierUri { get; set; }
        public string Summary { get; set; }
        public int LangId { get; set; } = 1;
        ///////////////////////////////////////////////////////////////////
        public Lang Lang { get; set; }
        public List<Subtitle> Subtitles { get; set; }
    }
}
