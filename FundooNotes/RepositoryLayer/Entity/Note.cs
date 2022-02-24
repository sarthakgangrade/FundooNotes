using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RepositoryLayer.Entity
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int NotesId { get; set; }
        
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsReminder { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string color { get; set; }
        public bool IsTrash { get; set; }
        public bool IsArchive { get; set; }
        public bool IsPin { get; set; }
        //public virtual ICollection<Label> Label { get; set; }
        //public virtual IList<Label> Label { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("User")]
        public int? userId { get; set; }

    }
}