using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public long mobilNo { get; set; }
        public string password { get; set; }
        public string cpassword { get; set; }
        public virtual ICollection<Note> Note { get; set; }
        public virtual ICollection<Label> Label{ get; set; }
        public virtual IList<UserAddress> UserAddress { get; set; }

    }
}
