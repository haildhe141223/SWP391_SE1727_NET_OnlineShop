using Microsoft.AspNetCore.Identity;
using SWP391.OnlineShop.Core.Models.Entities;

namespace SWP391.OnlineShop.Core.Models.Identities;

public class User : IdentityUser<int>
{
    public string Image { get; set; }

    public User()
    {
        FeedBacks = new HashSet<FeedBack>();
        Orders = new HashSet<Order>();
        UserVouchers = new HashSet<UserVoucher>();
        Vouchers = new HashSet<Voucher>();
		Addresses = new HashSet<Address>();
    }
    public ICollection<FeedBack> FeedBacks { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<UserVoucher> UserVouchers { get; set; }
    public ICollection<Voucher> Vouchers { get; set; }
    public ICollection<Address> Addresses { get; set; }
}