using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.ViewModels
{
    public class VocCardVM
    {
        public Voc Voc { get; set; } = new Voc();
        public UserVoc UserVoc { get; set; } = new UserVoc();
        public List<VocAudio> VocAudios { get; set; } = new List<VocAudio>();
        public List<Definition> Definitions { get; set; } = new List<Definition>();
        public string SayIt { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();
        public List<Synonym> Synonyms { get; set; } = new List<Synonym>();
        public List<Translate> Translates { get; set; } = new List<Translate>();
        public List<Subtitle> Subtitles { get; set; } 
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Exception { get; set; }
    }
}
