namespace SWP391.OnlineShop.Common.Constraints
{
    public class ImageConstraints
    {
        //// Uppercase ImageExtentions
        public const string JPG = ".JPG";
        public const string JPEG = ".JPEG";
        public const string PNG = ".PNG";
        public const string GIF = ".GIF";

        //// Lowercase ImageExtentions
        public const string jpg = ".jpg";
        public const string jpeg = ".jpeg";
        public const string png = ".png";
        public const string gif = ".gif";

        public static List<string> imageExtentionLists = new List<string>
        {
            JPG,
            JPEG,
            PNG,
            GIF,

            jpg,
            jpeg,
            png,
            gif
        };
    }
}
