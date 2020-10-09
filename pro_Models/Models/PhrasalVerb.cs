using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class PhrasalVerb
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        /////////////////////////////////////////////////
        public List<VocsPhrasalVerbs> VocsPhrasalVerbs { get; set; }
    }
}
