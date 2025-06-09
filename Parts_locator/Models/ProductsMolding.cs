using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.Data
{
    internal class ProductsMolding
    {
        public string partnum { get; set; }
        public string Modelname { get; set; }
        public string RotorBush { get; set; }
        public string ShaftPartnum { get; set; }
        public string ShaftBushPartnum { get; set; }
        public int Quantity { get; set; } = 0;
        public int Racks { get; set; }


        public static DataTable SearchProductLocation(string partnum)
        {
            GlobalDb db = new GlobalDb();

            var searchsql = "SELECT m.Type, b.PartNumber, m.ModelName, m.RotorBush, " + 
	                            "m.ShaftPartnum, m.ShaftBushAssyPartnum, " +
                                "b.Racks, b.Quantity, i.Sample_img " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation b " +
                            "ON m.PartNumber = b.PartNumber " +
                            "LEFT JOIN Parts_MoldingRawImage i ON i.PartNumber = m.PartNumber " +
                            "WHERE m.PartNumber = '" + partnum + "'";

            Debug.WriteLine(searchsql);

            return db.GetData(searchsql);
        }

        public static DataTable getProductList()
        {

            GlobalDb db = new GlobalDb();

            var strsql = "SELECT b.PartNumber, m.ModelName, m.RotorBush, " +
                                "m.ShaftPartnum, m.ShaftBushAssyPartnum, " +
                                "b.Racks, b.Quantity " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation b " +
                            "ON m.PartNumber = b.PartNumber ";
            return db.GetData(strsql);
        }

        public static DataTable getMoldingRowList(string partnum)
        {
            GlobalDb db = new GlobalDb();
            var strsql = "SELECT " +
                           "m.PartNumber, m.ModelName, m.RotorBush, " +
                           "m.ShaftPartnum, m.ShaftBushAssyPartnum, m.type, " +
                           "p.Racks, p.Quantity " +
                        "FROM Part_MoldingBushParts m " +
                        "INNER JOIN Part_ProductBushLocation p  " +
                        "ON m.PartNumber = p.PartNumber " +
                        "WHERE m.PartNumber = '" + partnum + "'";

            return db.GetData(strsql);
        }

        public static DataTable getModelingRowByType(int bushtype)
        {
            GlobalDb db = new GlobalDb();
            string filterstr = "";

            if (bushtype == 1)
            {
                filterstr = ", m.RotorBush, m.ShaftPartnum ";
            }
            else
            {
                filterstr = ", m.ModelName ";
            }

            string strsql = "SELECT m.PartNumber, p.Racks, p.Quantity " +
                            "" + filterstr + " " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation p " +
                            "ON m.PartNumber = p.PartNumber " +
                            "WHERE m.Type = " + bushtype + " ";

            Debug.WriteLine(strsql);

            DataTable td = db.GetData(strsql);
            return td;
        }
    }
}
