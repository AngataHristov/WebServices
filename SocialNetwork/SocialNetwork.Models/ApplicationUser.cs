﻿namespace SocialNetwork.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationUser : IdentityUser, IUser
    {
        private ICollection<Post> ownPosts;
        private ICollection<Post> wallPosts;
        private ICollection<PostLike> postLikes;

        public ApplicationUser()
        {
            this.ownPosts = new HashSet<Post>();
            this.wallPosts = new HashSet<Post>();
            this.postLikes = new HashSet<PostLike>();
        }

        public string Location { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<Post> OwnPosts
        {
            get
            {
                return this.ownPosts;
            }

            set
            {
                this.ownPosts = value;
            }
        }

        public virtual ICollection<Post> WallPosts
        {
            get
            {
                return this.wallPosts;
            }

            set
            {
                this.wallPosts = value;
            }
        }

        public virtual ICollection<PostLike> PostLikes
        {
            get
            {
                return this.postLikes;
            }

            set
            {
                this.postLikes = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(
            UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(
                this, authenticationType);

            return userIdentity;
        }
    }
}
