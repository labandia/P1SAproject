using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ProgramPartListWeb.Utilities.DataAccess
{
    public static class NetworkHelper
    {
        public static (string HostName, List<string> ServerIPs) GetDomainInfo(HttpRequestBase request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Get the host name (domain)
            string hostName = request.Url.Host;

            // Prepare a list to hold IP addresses
            List<string> ipAddresses = new List<string>();

            try
            {
                // Resolve the domain to IP addresses
                IPHostEntry hostEntry = Dns.GetHostEntry(hostName);

                foreach (var ip in hostEntry.AddressList)
                {
                    // Only add IPv4 addresses (optional)
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ipAddresses.Add(ip.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception if domain cannot be resolved
                ipAddresses.Add($"Error resolving IP: {ex.Message}");
            }

            return (hostName, ipAddresses);
        }
    }
}