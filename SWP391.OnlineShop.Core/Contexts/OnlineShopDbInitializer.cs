using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Contexts;

public class OnlineShopDbInitializer
{
    public void Seed()
    {
        using var context = new OnlineShopContext();

        if (context.Users.Any())
        {
            return;
        }

        #region Add Roles

        var roles = new List<Role>
            {
                new() {Name = RoleConstraints.Admin, NormalizedName = RoleConstraints.Admin.ToUpper()},
                new() {Name = RoleConstraints.Guest, NormalizedName = RoleConstraints.Guest.ToUpper()},
                new() {Name = RoleConstraints.Customer, NormalizedName = RoleConstraints.Customer.ToUpper()},
                new() {Name = RoleConstraints.Marketing, NormalizedName = RoleConstraints.Marketing.ToUpper()},
                new() {Name = RoleConstraints.Sale, NormalizedName = RoleConstraints.Sale.ToUpper()},
                new() {Name = RoleConstraints.SaleManager, NormalizedName = RoleConstraints.SaleManager.ToUpper()},
            };

        context.Roles.AddRange(roles);

        #endregion

        #region Add Admin

        var admin = new User
        {
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            NormalizedEmail = "ADMIN@GMAIL.COM",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            SecurityStamp = Guid.NewGuid().ToString(),
            // 123478@Kid
            PasswordHash = "AQAAAAEAACcQAAAAEKkkFTr9F0F9aDf0CLkOu+OW5NtKBnCD0WV3xXbhQdy0CGbrlA4wxDYHkuf3GYO/JA==",
        };

        context.Users.Add(admin);

        #endregion

        context.SaveChanges();

        #region Add UserRoles

        var userRole = new UserRole
        {
            UserId = admin.Id,
            RoleId = roles[0].Id
        };

        context.UserRoles.Add(userRole);

        #endregion

        context.SaveChanges();
    }
}