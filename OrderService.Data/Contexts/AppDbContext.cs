using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities.Commons;
using OrderService.Domain.Entities.Orders;
using OrderService.Domain.Entities.Porducts;
using OrderService.Domain.Entities.Users;

namespace OrderService.Data.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Attachment> Attachments { get; set; }
}
