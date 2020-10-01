using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class VocVM
    {
        [ValidateComplexType]
        public Voc Voc { get; set; } = new Voc();
        public string Exception { get; set; }
    }
}
