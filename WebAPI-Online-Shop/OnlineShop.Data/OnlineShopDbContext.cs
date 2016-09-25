namespace OnlineShop.Data
{
    using System;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;

    public class OnlineShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public OnlineShopDbContext()
            : base("name=OnlineShopConnectionString")
        {
        }

        public virtual IDbSet<Ad> Ads { get; set; }

        public virtual IDbSet<AdType> AdTypes { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public static OnlineShopDbContext Create()
        {
            return new OnlineShopDbContext();
        }
    }
}
