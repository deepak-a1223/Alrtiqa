using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(x => x.Roles)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<Roles>().HasData(
                new Roles { Id = 1, RoleName = "Admin", IsActive = true},
                new Roles { Id = 2, RoleName = "ReportManager", IsActive = true}
            );
        }
    }
}
