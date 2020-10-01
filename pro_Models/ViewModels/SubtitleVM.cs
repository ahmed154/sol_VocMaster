using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class SubtitleVM
    {
        [ValidateComplexType]
        public Subtitle Subtitle { get; set; } 
        public string Exception { get; set; }
    }
}
