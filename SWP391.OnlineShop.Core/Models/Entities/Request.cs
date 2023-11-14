using SWP391.OnlineShop.Core.Models.BaseEntities;
using SWP391.OnlineShop.Core.Models.Enums;
using SWP391.OnlineShop.Core.Models.Identities;

namespace SWP391.OnlineShop.Core.Models.Entities;

public class Request : BaseEntity<int>
{
    public string RequestData { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public string Reply { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}