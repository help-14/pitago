using System.Net.NetworkInformation;

namespace Pitago.Utils
{
    public static class Network
    {

        /// <summary>
        /// Check if  the computer is connected to the internet
        /// </summary>
        /// <returns>bool</returns>
        public static bool IsConnected()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

    }
}
