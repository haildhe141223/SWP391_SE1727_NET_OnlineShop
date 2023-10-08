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
			new Province(){ProvinceName= "Hà Nội"},
			new Province(){ProvinceName= "Hà Giang"},
			new Province(){ProvinceName= "Cao Bằng"},
			new Province(){ProvinceName= "Bắc Kạn"},
			new Province(){ProvinceName= "Tuyên Quang"},
			new Province(){ProvinceName= "Lào Cai"},
			new Province(){ProvinceName= "Điện Biên"},
			new Province(){ProvinceName= "Lai Châu"},
			new Province(){ProvinceName= "Sơn La"},
			new Province(){ProvinceName= "Yên Bái"},
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
			new District(){ DistrictName = "Quận Ba Đình", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Hoàn Kiếm", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Tây Hồ", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Long Biên", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Cầu Giấy", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Đống Đa", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Hai Bà Trưng", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Hoàng Mai", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Thanh Xuân", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Sóc Sơn", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Đông Anh", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Gia Lâm", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Nam Từ Liêm", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Thanh Trì", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Bắc Từ Liêm", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Mê Linh", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Quận Hà Đông", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Thị xã Sơn Tây", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Ba Vì", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Phúc Thọ", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Đan Phượng", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Hoài Đức", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Quốc Oai", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Thạch Thất", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Chương Mỹ", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Thanh Oai", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Thường Tín", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Phú Xuyên", ProvinceId = provinces[0].Id},
			new District(){DistrictName = "Huyện Ứng Hòa", ProvinceId = provinces[0].Id},
		};

		#endregion
		context.Districts.AddRange(districts);
		context.SaveChanges();

		#region Add wards
		if (context.Wards.Any())
		{
			return;
		}

		var wards = new List<Ward>()
		{
			new Ward(){ WardName= "Phường Phúc Xá", DistrictId = districts[0].Id},
			new Ward(){ WardName= "Phường Trúc Bạch", DistrictId = districts[0].Id},
			new Ward(){ WardName= "Phường Vĩnh Phúc", DistrictId = districts[0].Id},
			new Ward(){ WardName= "Phường Đồng Xuân", DistrictId = districts[1].Id},
			new Ward(){ WardName= "Phường Hàng Mã", DistrictId = districts[1].Id},
			new Ward(){ WardName= "Phường Nhật Tân", DistrictId = districts[2].Id},
			new Ward(){ WardName= "Phường Tứ Liên", DistrictId = districts[2].Id},
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