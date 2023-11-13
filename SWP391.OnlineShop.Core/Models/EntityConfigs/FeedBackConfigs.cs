using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class FeedBackConfigs : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> builder)
    {
        builder.HasOne<User>(fb => fb.User)
            .WithMany(u => u.FeedBacks)
            .HasForeignKey(fb => fb.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Product>(fb => fb.Product)
            .WithMany(p => p.FeedBacks)
            .HasForeignKey(fb => fb.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(fb => fb.RatedStar)
            .HasPrecision(18, 2);
    }
}