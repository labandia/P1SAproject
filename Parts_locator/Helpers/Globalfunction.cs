using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Data
{
    internal class Globalfunction
    {
        public static int Pallet_number(string pname)
        {
            string[] palletNames = { "A", "B", "C", "D", "E", "F", "H", "I", "J", "L", "M", "N", "O", "P", "R", "S", "T", "U" };

            int index = Array.IndexOf(palletNames, pname);

            return index >= 0 ? index + 1 : 0; // +1 to match your original numbering
        }
    }
}
