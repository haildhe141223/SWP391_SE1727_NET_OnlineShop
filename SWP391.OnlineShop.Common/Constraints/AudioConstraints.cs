namespace SWP391.OnlineShop.Common.Constraints
{
    public class AudioConstraints
    {
        //// Uppercase ImageExtensions
        public const string Mp3 = ".MP3";
        public const string Aac = ".AAC";
        public const string Ogg = ".OGG";
        public const string FLac = ".FLAC";
        public const string ALac = ".ALAC";
        public const string Wav = ".WAV";
        public const string M4A = ".M4A";
        public const string Voc = ".VOC";
        public const string WEbm = ".WEBM";

        //// Lowercase ImageExtensions
        public const string mp3 = ".mp3";
        public const string aac = ".aac";
        public const string ogg = ".ogg";
        public const string flac = ".flac";
        public const string alac = ".alac";
        public const string wav = ".wav";
        public const string m4a = ".m4a";
        public const string voc = ".voc";
        public const string webm = ".webm";

        public static List<string> AudioExtensionLists = new List<string>
        {
            Mp3,
            Aac,
            Ogg,
            FLac,
            ALac,
            Wav,
            M4A,
            Voc,
            WEbm,

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
