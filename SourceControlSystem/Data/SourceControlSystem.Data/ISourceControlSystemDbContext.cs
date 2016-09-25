namespace SourceControlSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Models;

    public interface ISourceControlSystemDbContext
    {
        IDbSet<SoftwareProject> Projects { get; set; }

        IDbSet<Commit> Commits { get; set; }

        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity)
            where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}
