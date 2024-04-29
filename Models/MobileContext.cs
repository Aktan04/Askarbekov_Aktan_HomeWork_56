using Microsoft.EntityFrameworkCore;

namespace AuthProduct.Models;

public class MobileContext : DbContext
{
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<PhoneReview> PhoneReviews { get; set; }
    
    public MobileContext(DbContextOptions<MobileContext> options) : base(options) { }
}