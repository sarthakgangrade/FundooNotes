using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class UserAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int AddressId { get; set; }

        [ForeignKey("User")]
        public int? userId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public virtual User User { get; set; }

    }
}
