namespace SocialNetwork.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [Required]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [ForeignKey("Author")]
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }
    }
}
