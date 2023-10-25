namespace SWP391.OnlineShop.Common.Constraints
{
    public class AudioConstraints
    {
        //// Uppercase ImageExtentions
        public const string MP3 = ".MP3";
        public const string AAC = ".AAC";
        public const string OGG = ".OGG";
        public const string FLAC = ".FLAC";
        public const string ALAC = ".ALAC";
        public const string WAV = ".WAV";
        public const string M4A = ".M4A";
        public const string VOC = ".VOC";
        public const string WEBM = ".WEBM";

        //// Lowercase ImageExtentions
        public const string mp3 = ".mp3";
        public const string aac = ".aac";
        public const string ogg = ".ogg";
        public const string flac = ".flac";
        public const string alac = ".alac";
        public const string wav = ".wav";
        public const string m4a = ".m4a";
        public const string voc = ".voc";
        public const string webm = ".webm";

        public static List<string> audioExtentionLists = new List<string>
        {
            MP3,
            AAC,
            OGG,
            FLAC,
            ALAC,
            WAV,
            M4A,
            VOC,
            WEBM,

            mp3,
            aac,
            ogg,
            flac,
            alac,
            wav,
            m4a,
            voc,
            webm
        };
    }
}
