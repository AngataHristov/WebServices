namespace SocialNetwork.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class SocialNetworkDbContext : IdentityDbContext<ApplicationUser>
    {
        public SocialNetworkDbContext()
           : base("SocialNetworkConnectionString")
        {
        }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<PostLike> Likes { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public static SocialNetworkDbContext Create()
        {
            return new SocialNetworkDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.WallPosts)
                .WithRequired(p => p.WallOwner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.OwnPosts)
                .WithRequired(p => p.Author)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
