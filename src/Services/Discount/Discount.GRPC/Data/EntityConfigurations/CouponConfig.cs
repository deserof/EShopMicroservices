using Discount.GRPC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Discount.GRPC.Data.EntityConfigurations;

public class CouponConfig : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable(nameof(Coupon));
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProductName);
    }
}