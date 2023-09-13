using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class VoucherConfigs : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        builder.HasOne<User>(v => v.User)
            .WithMany(u => u.Vouchers)
            .HasForeignKey(v => v.CreatedBy)
            .OnDelete(DeleteBehavior.Cascade);
    }
}