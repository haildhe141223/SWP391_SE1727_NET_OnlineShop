namespace SWP391.OnlineShop.Common.Constraints;

public class RequestConstraints
{
    public const string Approve = "Approve";
    public const string Reject = "Reject";
    public const string Pending = "Pending";

    public static readonly List<string> RequestValidStatus = new()
    {
        Approve,
        Reject,
        Pending
    };

}