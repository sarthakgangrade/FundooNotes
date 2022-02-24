using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Collab
{
    public class CollabModel
    {
        [Required]
        public string CollabEmail { get; set; }

    }
}
