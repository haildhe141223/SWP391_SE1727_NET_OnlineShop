using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class ProductVoucherConfigs : IEntityTypeConfiguration<ProductVoucher>
{
    public void Configure(EntityTypeBuilder<ProductVoucher> builder)
    {
        builder.HasOne<Product>(pv => pv.Product)
            .WithMany(p => p.ProductVouchers)
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Voucher>(pv => pv.Voucher)
            .WithMany(v => v.ProductVouchers)
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}