﻿namespace SWP391.OnlineShop.Core.Models.Enums;

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
    WaitingShipperToDeliverUnPaid = 3,
    WaitingShipperToDeliver = 4,
    Delivering = 5,
    Delivered = 6,
    Received = 7,
    DeliveringUnPaid = 8,
    Canceled = 9
}

public enum VoucherType
{
    Percent = 0,
    Money = 1
}

public enum ProductType
{
    InStock = 0,
    SoldOut = 1,
    InComing = 2,
    Event = 3
}

public enum RequestType
{
    RequestToBecomeSale,
    RequestToBecomeSaleManager,
    RequestToBecomeMarketing,
}

public enum RequestStatus
{
    Submitted,
    Pending,
    Approved,
    Rejected
}