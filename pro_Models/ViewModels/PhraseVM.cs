using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class PhraseVM
    {
        [ValidateComplexType]
        public Phrase Phrase { get; set; } = new Phrase();
        public string Exception { get; set; }
    }
}
