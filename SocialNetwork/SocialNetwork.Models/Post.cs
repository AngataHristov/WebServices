namespace SocialNetwork.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Post
    {
        private ICollection<Comment> comments;
        private ICollection<PostLike> likes;

        public Post()
        {
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<PostLike>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        [Required]
        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        [ForeignKey("Author")]
        [Required]
        public string AuthorId { get; set; }

        public virtual ApplicationUser Author { get; set; }

        [ForeignKey("WallOwner")]
        [Required]
        public string WallOwnerId { get; set; }

        public virtual ApplicationUser WallOwner { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get
            {
                return this.comments;
            }

            set
            {
                this.comments = value;
            }
        }

        public virtual ICollection<PostLike> Likes
        {
            get
            {
                return this.likes;
            }

            set
            {
                this.likes = value;
            }
        }
    }
}
