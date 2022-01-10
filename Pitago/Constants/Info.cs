namespace Pitago.Constants
{
    public static class Info
    {

        /// <summary>
        /// Link to the newest version txt on Github
        /// </summary>
        public const string NewestVersionUrl =
#if BETA
            "https://raw.githubusercontent.com/help-14/pitago/main/version/beta.txt";
#else 
            "https://raw.githubusercontent.com/help-14/pitago/main/version/stable.txt";
#endif

        /// <summary>
        /// Link to download the newest version
        /// </summary>
        public const string DownloadUrl = "https://github.com/help-14/pitago/releases";
    }
}
