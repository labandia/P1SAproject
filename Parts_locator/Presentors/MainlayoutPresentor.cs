using Parts_locator.Models;
using Parts_locator.View.Rotor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Presentors
{
    public class MainlayoutPresentor
    {
        private readonly IMainlayoutView _view;
        private readonly IProductRepository _res;

        public MainlayoutPresentor(IMainlayoutView view, IProductRepository res)
        {
            _view=view;
            _res=res;
        }

        // Load the masterlist of the products
        public DataTable LoadProductslist()
        {
            return  _res.GetAllProducts();
        }

        // Load the masterlist of the products
        public DataTable LoadMonitoringIn()
        {
            return _res.GetAllProducts();
        }

      
    }
}
