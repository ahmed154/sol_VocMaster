using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace pro_Models.Models
{
    public class UserVoc
    {
        public string UserId { get; set; }
        public int VocId { get; set; }
        public bool Study { get; set; }
        public int Repetition { get; set; }
        public int Success { get; set; }
        public int LevelCounter { get; set; }
        public int Level { get; set; } = 1;
        public DateTime NextReviewTime { get; set; } = DateTime.UtcNow;
        /////////////////////////////////////////////////////////////////////////////
        public User User { get; set; }
        public Voc Voc { get; set; }

    }
}
