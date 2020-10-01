using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class PickNewVM
    {
        [ValidateComplexType]

        public string UserId { get; set; }
        public string UserName { get; set; }
        public int LastVocId { get; set; }
        public int Take { get; set; } = 30;

        public List<Voc> Vocs { get; set; } = new List<Voc>();
        public string Exception { get; set; }
    }
}
