using System.Diagnostics;
using System.Reflection;

namespace Pitago.Utils
{
    public static class Update
    {

        /// <summary>
        /// Return current version from app assembly
        /// </summary>
        /// <returns>Version?</returns>
        public static Version? CurrentVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        /// <summary>
        /// Return newest version from Github
        /// </summary>
        /// <returns>Task<Version?></returns>
        public static async Task<Version?> NewestVersion()
        {
            try
            {
                var http = new HttpClient();
                var result = await http.GetStringAsync(Constants.Info.NewestVersionUrl);
                if (string.IsNullOrEmpty(result)) return null;
                return new Version(result);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Check if newest version is greater than current version
        /// </summary>
        /// <returns>Task<bool></returns>
        public static async Task<bool> CanUpdate()
        {
            if (!Network.IsConnected()) return false;

            var currentVersion = CurrentVersion();
            if (currentVersion == null) return true;
            var newestVersion = await NewestVersion();
            if(newestVersion == null) return false;
            return newestVersion.Major > currentVersion.Major ||
                (newestVersion.Major == currentVersion.Major && newestVersion.Minor > currentVersion.Minor) ||
                (newestVersion.Major == currentVersion.Major && newestVersion.Minor == currentVersion.Minor && newestVersion.Build > currentVersion.Build);
        }

        /// <summary>
        /// Run update task
        /// For now, open download link for the newest version
        /// </summary>
        public static void RunUpdate()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Constants.Info.DownloadUrl,
                UseShellExecute = true
            });
        }

    }
}
