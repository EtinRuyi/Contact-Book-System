using ContactBookAPI.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactBookAPI.Data
{
    public class ContactBookAPIDbContext : IdentityDbContext<User>
    {
        public ContactBookAPIDbContext()
        {

        }
        public ContactBookAPIDbContext(DbContextOptions<ContactBookAPIDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source =.; Initial Catalog = ContactDb; integrated security= True; Encrypt = False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
        }

        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                  new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                  new IdentityRole() { Name = "Regular", ConcurrencyStamp = "2", NormalizedName = "Regular" }
                );
        }
    }
}
