namespace SourceControlSystem.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Common.Constants;

    public class SoftwareProject
    {
        private ICollection<User> users;
        private ICollection<Commit> commits;

        public SoftwareProject()
        {
            this.users = new HashSet<User>();
            this.commits = new HashSet<Commit>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(ValidationConstants.MaxProjectName)]
        [Required]
        public string Name { get; set; }

        [MaxLength(ValidationConstants.MaxProjectDescription)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [DefaultValue(false)]
        public bool IsPrivate { get; set; }

        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }

        public virtual ICollection<Commit> Commits
        {
            get { return this.commits; }
            set { this.commits = value; }
        }
    }
}
