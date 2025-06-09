using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Web;
using System.Web.Mvc;

namespace PMACS_V2.Areas.Attendance.Controllers
{
    public class V2Controller : Controller
    {
        // GET: Attendance/V2
        public ActionResult TimeInandOut()
        {
            string clientIp = GetLocalIPv4();
            ViewBag.ClientIP = clientIp;
            return View();
        }






        public string GetClientLanIp()
        {
            string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                // In case of multiple IPs (client, proxy1, proxy2)
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    ipAddress = addresses[0];
                }
            }
            else
            {
                ipAddress = Request.ServerVariables["REMOTE_ADDR"];
            }

            // If it is IPv6 localhost, map to IPv4 localhost
            if (ipAddress == "::1")
            {
                ipAddress = "127.0.0.1";
            }

            return ipAddress;
        }


        public static string GetLocalIPv4()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only get interfaces that are Up and are Ethernet or Wireless
                if (ni.OperationalStatus == OperationalStatus.Up &&
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet ||
                     ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211))
                {
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "No IPv4 Address Found!";
        }

    }
}