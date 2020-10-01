using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class IdiomVM
    {
        [ValidateComplexType]
        public Idiom Idiom { get; set; } = new Idiom();
        public string Exception { get; set; }
    }
}
