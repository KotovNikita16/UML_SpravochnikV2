using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMLCatalog.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace UMLCatalog.Contexts
{
    public class UMLCatalogContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UMLElement> UMLElements { get; set; }
        public UMLCatalogContext(DbContextOptions<UMLCatalogContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";

            string adminEmail = "admin@example.com";
            string adminPassword = "Abc123456!";

            // добавляем роли
            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
        }
    }
}
