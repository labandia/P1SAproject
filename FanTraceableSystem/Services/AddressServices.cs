using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Linq;
using System.Windows.Forms;

namespace FanTraceableSystem.Services
{
    public static class AddressServices
    {
        public static int GetIpAddresSegment()
        {
            int octet = 0;
            string ipAddress = Dns.GetHostEntry(Dns.GetHostName())
                      .AddressList
                      .FirstOrDefault(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                      ?.ToString();

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string thirdPart = ipAddress.Split('.')[2]; // gets "1"
                octet = int.Parse(thirdPart);
                //MessageBox.Show(thirdPart);
            }

            return octet;

        }
    }
}
