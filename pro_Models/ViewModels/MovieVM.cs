using pro_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.ViewModels
{
    public class MovieVM
    {
        [ValidateComplexType]
        public Movie Movie { get; set; } = new Movie();
        public string Exception { get; set; }
    }
}
