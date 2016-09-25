namespace PetStore.Data
{
    using System.Data.Entity;

    using Models;

    public class PetStoreDbContext : DbContext
    {
        public PetStoreDbContext()
            : base("DefaultConnection")
        {
        }

        public virtual IDbSet<Pet> Pets { get; set; }
    }
}
