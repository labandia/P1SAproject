using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface IStockClient
    {
        void receiveStockAlert(string partName, int quantity);
    }
}
