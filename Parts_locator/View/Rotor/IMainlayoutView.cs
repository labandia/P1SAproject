using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.View.Rotor
{
    public interface IMainlayoutView
    {
        DataTable SetProductList();
        DataTable SetMonitoringIn();
        DataTable SetMonitoringOut();    
    }
}
