using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Label
{
    public class LabelResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string LabelName { get; set; }
        public int userId { get; set; }
        public int NotesId { get; set; }



        public string name { get; set; }
        public string email { get; set; }
        public string color { get; set; }
        public DateTime CreatedDate { get; set; }


    }
}
