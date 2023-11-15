using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class OrderDetailConfigs : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasOne<Order>(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>(od => od.Product)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(fb => fb.SalePrice)
            .HasPrecision(18, 2);

        builder.Property(fb => fb.UnitPrice)
            .HasPrecision(18, 2);
    }
}