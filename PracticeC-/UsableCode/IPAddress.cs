using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PracticeC_.UsableCode
{
    public static class IPAddress
    {
        // GET SEGMENT OF IP ADDRESS    
        // SAMPLE 192.168.X.1  WHERE X IS THE SEGMENT WE WANT TO GET    
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
