namespace AuctionSite.Migrations
{
    using AuctionSite.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<AuctionSite.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AuctionSite.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Categories.AddOrUpdate( c => c.CategoryID,
                new Category() { CategoryID = 1, Name = "Automotive" },
                new Category() { CategoryID = 2, Name = "Beauty" },
                new Category() { CategoryID = 3, Name = "Books" },
                new Category() { CategoryID = 4, Name = "Electronics" },
                new Category() { CategoryID = 5, Name = "Health" },
                new Category() { CategoryID = 6, Name = "Home" },
                new Category() { CategoryID = 7, Name = "Movies" },
                new Category() { CategoryID = 8, Name = "Music" },
                new Category() { CategoryID = 9, Name = "Sports" },
                new Category() { CategoryID = 10, Name = "Other" }
            );

            CreateRoles(context, RoleActions.Admin, RoleActions.Moderator, RoleActions.Member);
            CreateUser(context, RoleActions.Admin, WebConfigurationManager.AppSettings["DefaultAdminUsername"], WebConfigurationManager.AppSettings["DefaultAdminPassword"]);
            CreateUser(context, RoleActions.Moderator, WebConfigurationManager.AppSettings["DefaultModeratorUsername"], WebConfigurationManager.AppSettings["DefaultModeratorPassword"]);
            CreateUser(context, RoleActions.Member, WebConfigurationManager.AppSettings["DefaultMemberUsername"], WebConfigurationManager.AppSettings["DefaultMemberPassword"]);
        }

        private void CreateRoles(ApplicationDbContext db, params string[] roles)
        {
            var roleStore = new RoleStore<IdentityRole>(db);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (var role in roles)
            {
                if (!db.Roles.Any(r => r.Name == role))
                {
                    var identityRole = new IdentityRole { Name = role };
                    roleManager.Create(identityRole);
                }
            }
        }

        private void CreateUser(ApplicationDbContext db, string role, string username, string password)
        {
            if (!db.Users.Any(u => u.UserName == username))
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new UserManager<ApplicationUser>(userStore);
                var applicationUser = new ApplicationUser { UserName = username, Email = username + "@email.com", EmailConfirmed = true };
                userManager.Create(applicationUser, password);
                userManager.AddToRole(applicationUser.Id, role);
            }
        }
    }
}
