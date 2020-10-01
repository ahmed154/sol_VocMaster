using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class QuoteVM
    {
        [ValidateComplexType]
        public Quote Quote { get; set; } = new Quote();
        public string Exception { get; set; }
    }
}
