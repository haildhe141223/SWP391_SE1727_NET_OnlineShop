using SWP391.OnlineShop.Common.Constraints;
using SWP391.OnlineShop.Core.Models.Entities;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Contexts;

public class OnlineShopDbInitializer
{
    public void Seed()
    {
        using var context = new OnlineShopContext();

        #region Add Province

        if (context.Provinces.Any())
        {
            return;
        }

        var provinces = new List<Province>()
        {
            new ()  {ProvinceName= "Hà Nội"},
            new ()  {ProvinceName= "Hà Giang"},
            new ()  {ProvinceName= "Cao Bằng"},
            new ()  {ProvinceName= "Bắc Kạn"},
            new ()  {ProvinceName= "Tuyên Quang"},
            new ()  {ProvinceName= "Lào Cai"},
            new ()  {ProvinceName= "Điện Biên"},
            new ()  {ProvinceName= "Lai Châu"},
            new ()  {ProvinceName= "Sơn La"},
            new ()  {ProvinceName= "Yên Bái"},
        };

        #endregion
        context.Provinces.AddRange(provinces);
        context.SaveChanges();

        if (context.Districts.Any())
        {
            return;
        }
        #region Add District		
        var districts = new List<District>()
        {
            new ()  {DistrictName = "Quận Ba Đình", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Hoàn Kiếm", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Tây Hồ", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Long Biên", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Cầu Giấy", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Đống Đa", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Hai Bà Trưng", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Hoàng Mai", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Thanh Xuân", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Sóc Sơn", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Đông Anh", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Gia Lâm", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Nam Từ Liêm", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Thanh Trì", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Bắc Từ Liêm", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Mê Linh", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Quận Hà Đông", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Thị xã Sơn Tây", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Ba Vì", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Phúc Thọ", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Đan Phượng", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Hoài Đức", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Quốc Oai", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Thạch Thất", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Chương Mỹ", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Thanh Oai", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Thường Tín", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Phú Xuyên", ProvinceId = provinces[0].Id},
            new ()  {DistrictName = "Huyện Ứng Hòa", ProvinceId = provinces[0].Id},
        };

        #endregion
        context.Districts.AddRange(districts);
        context.SaveChanges();

        #region Add wards
        if (context.Wards.Any())
        {
            return;
        }

        var wards = new List<Ward>
        {
            new ()  {WardName= "Phường Phúc Xá", DistrictId = districts[0].Id},
            new ()  {WardName= "Phường Trúc Bạch", DistrictId = districts[0].Id},
            new ()  {WardName= "Phường Vĩnh Phúc", DistrictId = districts[0].Id},
            new ()  {WardName= "Phường Đồng Xuân", DistrictId = districts[1].Id},
            new ()  {WardName= "Phường Hàng Mã", DistrictId = districts[1].Id},
            new ()  {WardName= "Phường Nhật Tân", DistrictId = districts[2].Id},
            new ()  {WardName= "Phường Tứ Liên", DistrictId = districts[2].Id},
        };

        context.Wards.AddRange(wards);
        context.SaveChanges();
        #endregion
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