using CommonLayer.Note;
using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    //[Keyless]
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int LabelId { get; set; }
        //[DataType(DataType.Text)]
        [Required]
        public string LabelName { get; set; }
        public virtual Note Notes { get; set; }
        [ForeignKey("Note")]
        public int? NotesId { get; set; }


        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int? userId { get; set; }
    }
}
