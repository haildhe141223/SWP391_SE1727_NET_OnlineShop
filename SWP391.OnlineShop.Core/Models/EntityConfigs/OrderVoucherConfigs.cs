using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class OrderVoucherConfigs : IEntityTypeConfiguration<OrderVoucher>
{
    public void Configure(EntityTypeBuilder<OrderVoucher> builder)
    {
        builder.HasOne<Order>(pv => pv.Order)
            .WithMany(p => p.OrderVouchers)
            .HasForeignKey(pv => pv.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne<Voucher>(pv => pv.Voucher)
            .WithMany(v => v.OrderVouchers)
            .HasForeignKey(pv => pv.VoucherId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}