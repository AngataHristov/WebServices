namespace SourceControlSystem.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Common.Constants;

    public class User : IdentityUser
    {
        private ICollection<Commit> commits;
        private ICollection<SoftwareProject> projects;

        public User()
        {
            this.commits = new HashSet<Commit>();
            this.projects = new HashSet<SoftwareProject>();
        }

        [MinLength(ValidationConstants.MinUserName)]
        [MaxLength(ValidationConstants.MaxUserName)]
        public string FirstName { get; set; }

        [MinLength(ValidationConstants.MinUserName)]
        [MaxLength(ValidationConstants.MaxUserName)]
        public string LastName { get; set; }

        public virtual ICollection<Commit> Commits
        {
            get { return this.commits; }
            set { this.commits = value; }
        }

        public virtual ICollection<SoftwareProject> Projects
        {
            get { return this.projects; }
            set { this.projects = value; }
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            return userIdentity;
        }
    }
}
