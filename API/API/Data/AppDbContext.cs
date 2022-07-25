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
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<RelationShipType> RelationShipType { get; set; }
        public DbSet<StudentAddress> StudentAddress { get; set; }
        public DbSet<StudentFamilyInfo> StudentFamily { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasOne(x => x.Roles)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            builder.Entity<Person>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Persons)
                .HasForeignKey(x => x.StudentId);

            builder.Entity<Person>()
                .HasOne(x => x.Relation)
                .WithMany(x => x.Persons)
                .HasForeignKey(x => x.RelationShipTypeId);

            builder.Entity<StudentAddress>()
                .HasOne(x => x.Student)
                .WithMany(x => x.Addresses)
                .HasForeignKey(x => x.StudentId);

            builder.Entity<StudentFamilyInfo>()
                .HasOne(x => x.Student)
                .WithOne(x => x.StudentFamily)
                .HasForeignKey<StudentFamilyInfo>(x => x.StudentId);

            builder.Entity<Roles>().HasData(
                new Roles { Id = 1, RoleName = "Admin", IsActive = true},
                new Roles { Id = 2, RoleName = "ReportManager", IsActive = true}
            );

            builder.Entity<RelationShipType>().HasData(
                new RelationShipType { RelationShipTypeId = 1, RelationType = "Father", Description = "Father Relation" },
                new RelationShipType { RelationShipTypeId = 2, RelationType = "Mother", Description = "Mother Relation" }
            );
        }
    }
}
