namespace SWP391.OnlineShop.Core.Models.Enums;

public enum Status
{
    Active = 0,
    Inactive = 1
}

public enum Gender
{
    Female = 0,
    Male = 1,
    Other = 2
}

public enum MailStatus
{
    New = 0,
    Sending = 1,
    Sent = 2,
    Failed = 3,
    Expired = 4,
    ConditionalPending = 5
}

public enum CategoryType
{
    ProductCategory = 0,
    PostCategory = 1
}

public enum OrderStatus
{
    InCartDetail = 0,
    InCartContact = 1,
    InCartCompletion = 2,
    WaitingConfirmBySaler = 3,
    WaitingShipperToDeliver = 4,
    Delivering = 5,
    Delivered = 6,
    Received = 7
}