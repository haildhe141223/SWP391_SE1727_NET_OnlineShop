using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.EntityConfigs;

public class RequestConfigs : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasOne<User>(r => r.User)
            .WithMany(u => u.Requests)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}