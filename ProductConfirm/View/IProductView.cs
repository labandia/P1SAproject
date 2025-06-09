using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductConfirm.View
{
    public interface IProductView
    {
        int Id { get; set; }
        string Partnum { get; set; }
        string Model_name { get; set; }
        string Machinepressure { get; set; }
        decimal Caulkmin { get; set; }
        decimal Caulkmax { get; set; }
        decimal shaftmin { get; set; }
        decimal Shaftmax { get; set; }
        decimal ShaftEdgemin { get; set; }
        decimal ShaftEdgemax { get; set; }
        decimal ShaftPull { get; set; }
        decimal BushPull { get; set; }

        DataTable GetAllProducts();
        DataTable GetMinandMaxProduct(int rotorid);
        DataTable GetShoporderlist();
        DataTable GetShoporderDetails(string shoporder, int ID);
        DataTable GetSummaryDataConfirmation();
        DataTable GetDataAndExportoExcel();
    }
}
