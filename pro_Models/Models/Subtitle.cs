using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pro_Models.Models
{
    public class Subtitle
    {
        public int Id { get; set; }
        [Required]
        public string Index { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndtTime { get; set; }
        public int Rank { get; set; }
        public string ClipUri { get; set; }
        [NotMapped]
        public string OnCloudClip
        {
            get
            {
                if (Id == 0 || MovieId == 0)
                {
                    return null;
                }
                else
                {
                    if (!string.IsNullOrEmpty(ClipUri)) return ClipUri;
                    return @$"{Helper.Helper.CdnUri}Movies/{MovieId.ToString().PadLeft(6, '0')}/Clips/{Id.ToString("0000")}.mp4";
                }
            }
        }
        public int MovieId { get; set; }
        //---------------------------------------------- relationships
        public Movie Movie { get; set; }
        public List<VocTest> VocTests { get; set; }
        public List<VocSubtitle> VocSubtitles { get; set; }
    }
}
