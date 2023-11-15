namespace SWP391.OnlineShop.Common.Constraints;

public class CommonConstraints
{
    public static readonly List<string> InvalidDictionary = new()
    {
        "\'",
        "\""
    };

    public const string JoinCharacter = "; ";
}