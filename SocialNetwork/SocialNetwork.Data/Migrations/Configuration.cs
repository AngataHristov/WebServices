namespace SocialNetwork.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class Configuration : DbMigrationsConfiguration<SocialNetworkDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SocialNetworkDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }

            var user = context.Users
                .FirstOrDefault(u => u.UserName == "angel");

            var moderatorRole = new IdentityRole()
            {
                Name = "Moderator",
            };

            var adminRole = new IdentityRole()
            {
                Name = "Admin",
            };

            context.Roles.Add(adminRole);
            context.Roles.Add(moderatorRole);
            context.SaveChanges();

            adminRole.Users.Add(new IdentityUserRole()
            {
                UserId = user.Id
            });

            context.SaveChanges();
        }
    }
}
