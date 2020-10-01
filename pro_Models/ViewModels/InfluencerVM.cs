using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class InfluencerVM
    {
        [ValidateComplexType]
        public Influencer Influencer { get; set; } = new Influencer();
        public string Exception { get; set; }
    }
}
