namespace SWP391.OnlineShop.Common.Constraints;

public class LoginKeyConstraints
{
    public const string RequiredTwoFactorKey = "REQUIRED-TWO-FACTOR";
    public const string RequiredEmailConfirm = "REQUIRED-EMAIL-CONFIRM";
    public const string RequiredLogin = "LOGIN";

    #region Valid Gmail Domain Email

    public static readonly List<string> ValidDomainGmail = new()
    {
        "gmail.com",
        "fpt.edu.vn",
        "fe.edu.vn"
    };

    #endregion

    #region External Login Constraints

    public const string GoogleLogin = "Google";
    public const string FacebookLogin = "Facebook";

    #endregion

    #region VietnameseDictionary

    public static readonly List<string> VietnameseDictionary = new()
    {
        "àåáâäãåąảạắặằẳăấầậẩ",
        "çćčĉ",
        "đ",
        "èéêëęẹẻếềệể",
        "ğĝ",
        "ĥ",
        "ìíîïıịỉ",
        "ĵ",
        "ł",
        "ñń",
        "òóôõöøőðơớợờỡốồộổọỏ",
        "ř",
        "śşšŝ",
        "ß",
        "Þ",
        "ùúûüŭůũủụ",
        "ýÿỹỷỵỳ",
        "żźž",
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ "
    };

    #endregion

    #region Default Password

    public const string DefaultPassword = "123478@Kid";

    #endregion
}