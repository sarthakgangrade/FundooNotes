using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        //public ICollection<Note.Note> Notes { get; set; }

    }
}
