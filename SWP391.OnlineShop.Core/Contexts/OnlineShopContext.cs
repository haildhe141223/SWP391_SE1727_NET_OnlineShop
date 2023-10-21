using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.EntityConfigs;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Contexts;

public class OnlineShopContext : IdentityDbContext<User, Role, int>
{
    public OnlineShopContext()
    {

    }

    public OnlineShopContext(DbContextOptions<OnlineShopContext> options)
        : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<ProductVoucher> ProductVouchers { get; set; }
    public DbSet<UserVoucher> UserVouchers { get; set; }
    public DbSet<Setting> Setting { get; set; }
    public DbSet<Slider> Slider { get; set; }



    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Custom Entities Configs
        builder.ApplyConfiguration(new FeedBackConfigs());
        builder.ApplyConfiguration(new OrderConfigs());
        builder.ApplyConfiguration(new OrderDetailConfigs());
        builder.ApplyConfiguration(new PostConfigs());
        builder.ApplyConfiguration(new PostTagConfigs());
        builder.ApplyConfiguration(new ProductConfigs());
        builder.ApplyConfiguration(new ProductTagConfigs());
        builder.ApplyConfiguration(new ProductVoucherConfigs());
        builder.ApplyConfiguration(new UserVoucherConfigs());
        builder.ApplyConfiguration(new VoucherConfigs());

        // Custom Identities Configs
        // Change name of identity tables
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();
            if (tableName != null && tableName.StartsWith("AspNet"))
            {
                entityType.SetTableName(tableName[6..]);
            }
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(OnlineShopConnectionStrings.DefaultConnectionString);
        }
    }

    public void BeforeComplete()
    {
        var dateNow = DateTime.Now;

        foreach (var entity in ChangeTracker.Entries())
        {
            if (entity.Entity is not IBaseEntity<int> baseEntity) continue;

            switch (entity.State)
            {
                case EntityState.Added:
                    baseEntity.CreatedDateTime = dateNow;
                    baseEntity.ModifiedDateTime = dateNow;
                    break;
                case EntityState.Modified:
                    baseEntity.ModifiedDateTime = dateNow;
                    break;
            }
        }
    }
}