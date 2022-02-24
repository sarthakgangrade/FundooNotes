using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Label
{
    public class LabelModel
    {
        [DataType(DataType.Text)]
        [Required]
        public string LabelName { get; set; }
        
    }
}
