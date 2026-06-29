namespace PaintManagementSystem.API.DataBase;

using Microsoft.EntityFrameworkCore;
using PaintManagementSystem.Models.Models;


public class PaintDbContext : DbContext
{
    public PaintDbContext(DbContextOptions<PaintDbContext> options) : base(options)
    {
    }

    public DbSet<PaintProduct> PaintProducts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<User>  Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<OrderList> OrderLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PaintProduct>()
            .OwnsOne(product => product.Specification);
    }
}