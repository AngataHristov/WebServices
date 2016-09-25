namespace SourceControlSystem.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class SourceControlSystemDbContext : IdentityDbContext<User>, ISourceControlSystemDbContext
    {
        public SourceControlSystemDbContext()
            : base("SourceControlSystemConnectionString", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<SoftwareProject> Projects { get; set; }

        public virtual IDbSet<Commit> Commits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SoftwareProject>()
                .HasMany(p => p.Users)
                .WithMany(u => u.Projects);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Commits);

            modelBuilder.Entity<SoftwareProject>()
               .HasMany(p => p.Commits)
               .WithRequired(c => c.SoftwareProject);

            modelBuilder.Entity<Commit>()
                .HasRequired(c => c.Author);

            base.OnModelCreating(modelBuilder);
        }

        public static SourceControlSystemDbContext Create()
        {
            return new SourceControlSystemDbContext();
        }
    }
}
