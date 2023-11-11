namespace SWP391.OnlineShop.Common.Extensions
{
    public static class EnumExtensionMethods
    {
        public static T ToEnumCustom<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
