using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class LangVM
    {
        [ValidateComplexType]
        public Lang Lang { get; set; } = new Lang();
        public string Exception { get; set; }
    }
}
