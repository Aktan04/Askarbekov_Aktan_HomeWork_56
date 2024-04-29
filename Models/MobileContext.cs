using Microsoft.EntityFrameworkCore;

namespace AuthProduct.Models;

public class MobileContext : DbContext
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<PhoneReview> PhoneReviews { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role {Id = 1, Name = "admin"});
        modelBuilder.Entity<Role>().HasData(new Role {Id = 2, Name = "user"});
        modelBuilder.Entity<User>().HasData(new User {Id = 1, Email = "admin@admin.admin", Password = "admin", RoleId = 1});
        base.OnModelCreating(modelBuilder);
    }
}