using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace pro_Models.Models
{
    public class Definition
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        public int VocId { get; set; }
        /// //////////////////////////////////////////////////
        public List<Example> Examples { get; set; }

    }
}
