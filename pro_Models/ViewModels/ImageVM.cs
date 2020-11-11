using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class ImageVM
    {
        [ValidateComplexType]
        public Image Image { get; set; } = new Image();
        public string UserName { get; set; }
        public string Exception { get; set; }
    }
}
