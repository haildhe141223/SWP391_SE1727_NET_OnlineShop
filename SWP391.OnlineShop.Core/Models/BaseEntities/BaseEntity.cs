using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.BaseEntities;

public class BaseEntity<TKey> : IBaseEntity<TKey>
{
    public TKey Id { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
}