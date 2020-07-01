namespace BonoLogin.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BonoLogin.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BonoLogin.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            /*

            var passWordHash = new PasswordHasher();
            string pass = passWordHash.HashPassword("admin1234");


            context.Users.AddOrUpdate(u => u.UserName, new ApplicationUser()
            {
                Email = "emisor@admin.com",
                UserName = "emisor@admin.com",
                PasswordHash = pass,
                SecurityStamp = Guid.NewGuid().ToString(),
            });


            //asignando rol a emisor admin

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var userId = userManager.FindByEmail("emisor@admin.com").Id;
            var resultado = roleManager.Create(new IdentityRole("Emisor"));
            if (userId != null) {
                resultado = userManager.AddToRole(userId, "Emisor");
            }

            context.DatosEmisor.AddOrUpdate(de => de.Alias, new DatosEmisor() { 
              Alias = "Default",
              Ianual = 0.0,
              Ir = 30.0,
              Pprima = 1.0,
              PCavali = 0.5,
              Pcolocacion = 0.5,
              Pestructuracion = 0.5,
              Pflotacion = 0.45,
              UserId = userId,
            });

            context.DatosEmisor.AddOrUpdate(de => de.Alias, new DatosEmisor()
            {
                Alias = "Default2",
                Ianual = 0.0,
                Ir = 30.0,
                Pprima = 1.0,
                PCavali = 0.5,
                Pcolocacion = 0.25,
                Pestructuracion = 0.45,
                Pflotacion = 0.15,
                UserId = userId,
            });

            context.SaveChanges();
            */
            
        }
    }
}
