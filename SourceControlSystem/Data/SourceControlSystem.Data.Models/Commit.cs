namespace SourceControlSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Constants;

    public class Commit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SourceCode { get; set; }

        public DateTime CreatedOn { get; set; }

        [MinLength(ValidationConstants.MinCommitMessage)]
        [MaxLength(ValidationConstants.MaxCommitMessage)]
        [Required]
        public string Message { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [ForeignKey("SoftwareProject")]
        public int SoftwareProjectId { get; set; }

        public virtual SoftwareProject SoftwareProject { get; set; }
    }
}
