using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Net.Sockets;
using System.Management;
using System.DirectoryServices.AccountManagement;

namespace ProgramPartListWeb.Utilities
{
    public static class ClientsInfo
    {
        public static string GetClientIpAddress()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return "Unknown";

            string ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ip) || ip.ToLower() == "unknown")
            {
                ip = context.Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                ip = ip.Split(',')[0].Trim();
            }

            if (string.IsNullOrEmpty(ip))
                return "Unknown";

            // Loopback (::1 or 127.0.0.1) - happens when testing locally
            if (ip == "::1" || ip == "127.0.0.1")
            {
                return GetLocalMachineIp();
            }

            // Normalize IPv6-mapped IPv4 addresses (::ffff:192.168.1.25 -> 192.168.1.25)
            if (ip.StartsWith("::ffff:"))
            {
                ip = ip.Substring(7);
            }

            return ip;
        }

        /// <summary>
        /// Queries the remote client PC directly via WMI to get the currently logged-in user.
        /// Does NOT rely on IIS Windows Authentication. Requires:
        ///  - The app pool account has admin/WMI query rights on client PCs (domain env)
        ///  - Remote Registry / WMI (DCOM, port 135) reachable from server to client
        ///  - Client PCs are domain-joined and on the same network
        /// </summary>
        public static (string ComputerName, string AccountName) GetComputerInfoViaWmi(string targetHostOrIp)
        {
            if (string.IsNullOrEmpty(targetHostOrIp) || targetHostOrIp == "Unknown")
                return ("Unknown", "Unknown");

            try
            {
                var connOptions = new ConnectionOptions
                {
                    Timeout = TimeSpan.FromSeconds(3)
                };

                var scope = new ManagementScope($@"\\{targetHostOrIp}\root\cimv2", connOptions);
                scope.Connect();

                var query = new ObjectQuery("SELECT Name, UserName FROM Win32_ComputerSystem");
                var enumOptions = new EnumerationOptions { Timeout = TimeSpan.FromSeconds(3) };

                using (var searcher = new ManagementObjectSearcher(scope, query, enumOptions))
                {
                    foreach (ManagementObject mo in searcher.Get())
                    {
                        string computerName = mo["Name"]?.ToString() ?? "Unknown";
                        string userName = mo["UserName"]?.ToString() ?? "Unknown";
                        return (computerName, userName);
                    }
                }

                return ("Unknown", "Unknown");
            }
            catch (Exception)
            {
                return ("Unknown", "Unknown");
            }
        }




        private static string GetLocalMachineIp()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPHostEntry entry = Dns.GetHostEntry(hostName);

                var ipv4 = entry.AddressList
                    .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork);

                return ipv4?.ToString() ?? "127.0.0.1";
            }
            catch (Exception)
            {
                return "127.0.0.1";
            }
        }

        /// <summary>
        /// Resolves a hostname/computer name from an IP address via reverse DNS.
        /// </summary>
        public static string GetHostName(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress) || ipAddress == "Unknown")
                return "Unknown";

            try
            {
                IPHostEntry entry = Dns.GetHostEntry(ipAddress);
                string hostName = entry.HostName;
                int dotIndex = hostName.IndexOf('.');
                return dotIndex > 0 ? hostName.Substring(0, dotIndex) : hostName;
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Gets the logged-in Windows account (DOMAIN\Username) of the CLIENT making the request.
        /// Requires Windows Authentication enabled in IIS + web.config, and Anonymous Authentication disabled.
        /// Returns "Unknown" if Windows Auth isn't active/configured correctly.
        /// </summary>
        public static string GetAccountName()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
                return "Unknown";

            // Preferred: current authenticated identity
            if (context.User?.Identity != null && context.User.Identity.IsAuthenticated
                && !string.IsNullOrEmpty(context.User.Identity.Name))
            {
                return context.User.Identity.Name; // e.g. FACTORYDOMAIN\jsmith
            }

            // Fallback: LOGON_USER server variable
            string logonUser = context.Request.ServerVariables["LOGON_USER"];
            if (!string.IsNullOrEmpty(logonUser))
                return logonUser;

            // Fallback: AUTH_USER (used with some auth providers)
            string authUser = context.Request.ServerVariables["AUTH_USER"];
            if (!string.IsNullOrEmpty(authUser))
                return authUser;

            return "Unknown";
        }

        /// <summary>
        /// Gets just the username portion, stripping the DOMAIN\ prefix.
        /// </summary>
        public static string GetAccountNameOnly()
        {
            string fullAccount = GetAccountName();
            if (fullAccount == "Unknown")
                return fullAccount;

            int backslashIndex = fullAccount.IndexOf('\\');
            return backslashIndex >= 0 ? fullAccount.Substring(backslashIndex + 1) : fullAccount;
        }
        /// <summary>
        /// Gets the Windows account the ASP.NET/IIS worker process itself is running under
        /// (the server-side equivalent of running "echo %USERNAME%" ON THE SERVER).
        /// This is almost always the app pool identity (e.g. "IIS APPPOOL\YourAppPool"),
        /// NOT the client/operator at the browser. Included for diagnostics only.
        /// </summary>
        public static string GetServerProcessUsername()
        {
            try
            {
                return $"{Environment.UserDomainName}\\{Environment.UserName}";
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }


        /// <summary>
        /// Looks up the AD email address for a given account name (DOMAIN\username or just username).
        /// Requires the app pool identity to have read access to Active Directory.
        /// </summary>
        public static string GetEmailFromAccountName(string accountName)
        {
            if (string.IsNullOrEmpty(accountName) || accountName == "Unknown")
                return "Unknown";

            try
            {
                string domain = null;
                string username = accountName;

                if (accountName.Contains("\\"))
                {
                    var parts = accountName.Split('\\');
                    domain = parts[0];
                    username = parts[1];
                }

                using (var context = string.IsNullOrEmpty(domain)
                    ? new PrincipalContext(ContextType.Domain)
                    : new PrincipalContext(ContextType.Domain, domain))
                {
                    using (var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, username))
                    {
                        if (user == null)
                            return "Unknown"; // account not found in AD

                        return string.IsNullOrEmpty(user.EmailAddress)
                            ? "NoEmailSet"   // account found, but Email attribute is blank in AD
                            : user.EmailAddress;
                    }
                }
            }
            catch (PrincipalServerDownException)
            {
                return "ADUnreachable"; // domain controller not reachable
            }
            catch (Exception)
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Convenience: resolves account name + email in one call.
        /// </summary>
        public static (string AccountName, string Email) GetAccountAndEmail(string computerName = null)
        {
            // Try Windows Auth first, fall back to WMI if a computer name is supplied
            string account = GetAccountName();

            //if (account == "Unknown" && !string.IsNullOrEmpty(computerName))
            //{
            //    account = GetLoggedInUserViaWmi(computerName);
            //}

            string email = GetEmailFromAccountName(account);

            return (account, email);
        }

        public static ClientInfo GetClientInfo()
        {
            string ip = GetClientIpAddress();
            string hostName = GetHostName(ip);
            string account = GetAccountName();

            return new ClientInfo
            {
                IpAddress = ip,
                ComputerName = hostName,
                AccountName = account
            };
        }
    }

    public class ClientInfo
    {
        public string ComputerName { get; set; }
        public string IpAddress { get; set; }
        public string AccountName { get; set; }
    }
}