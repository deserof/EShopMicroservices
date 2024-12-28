using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data;

public class DiscountContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; }

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coupon>().HasData(
            new Coupon { Id = 1, ProductName = "Iphone X", Description = "The best phone", Amount = 10 },
            new Coupon { Id = 2, ProductName = "Samsung S24", Description = "The worst phone", Amount = 20 });
    }
}