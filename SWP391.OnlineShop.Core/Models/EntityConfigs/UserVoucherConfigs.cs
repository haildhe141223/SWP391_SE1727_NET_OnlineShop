using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class UserVoucherConfigs : IEntityTypeConfiguration<UserVoucher>
{
    public void Configure(EntityTypeBuilder<UserVoucher> builder)
    {
        builder.HasOne<User>(uv => uv.User)
            .WithMany(u => u.UserVouchers)
            .HasForeignKey(uv => uv.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne<Voucher>(uv => uv.Voucher)
            .WithMany(v => v.UserVouchers)
            .HasForeignKey(uv => uv.VoucherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}