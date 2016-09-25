namespace SocialNetwork.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PostLike
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
