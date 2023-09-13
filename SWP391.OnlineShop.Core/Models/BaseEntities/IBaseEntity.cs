using SWP391.OnlineShop.Core.Models.Enums;

namespace SWP391.OnlineShop.Core.Models.BaseEntities;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
    Status Status { get; set; }
    DateTime CreatedDateTime { get; set; }
    DateTime ModifiedDateTime { get; set; }
}