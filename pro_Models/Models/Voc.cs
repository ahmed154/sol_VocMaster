using System;
using System.Collections.Generic;
using System.Text;

namespace pro_Models.Models
{
    public class Voc
    {
        public int Id { get; set; }
        
        public string Text { get; set; }
        public int Freq  { get; set; }
        ///////////////////////////////////////////////////////////
        public List<VocAudio> VocAudios { get; set; }
        public List<Definition> Definitions { get; set; }
        public List<Synonym> Synonyms { get; set; }
        public List<Translate> Translates { get; set; }
        public List<VocSubtitle> VocSubtitles { get; set; }
        public List<Image> Images { get; set; }
        public List<VocsQuotes> VocsQuotes { get; set; }
        public List<VocsIdioms> VocsIdioms { get; set; }
        public List<VocsPhrases> VocsPhrases { get; set; }
        public List<VocsPhrasalVerbs> VocsPhrasalVerbs { get; set; }
    }
}
