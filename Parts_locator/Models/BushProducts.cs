using DocumentFormat.OpenXml.Vml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Data;
using System.Data.SqlClient;

namespace Parts_locator.Models
{
    internal class BushProducts : Products
    {
        private string _Rotorbush;
        private string _ShaftPart;
        private int _Racks;

        public string RotorBush { get { return _Rotorbush; } set { _Rotorbush = value; } }
        public string ShaftPartnum { get { return _ShaftPart; } set { _ShaftPart = value; } }
        public int Racks { get { return _Racks; } set { _Racks = value; } }



        public  DataTable SearchProductLocation()
        {
            GlobalDb db = new GlobalDb();

            var searchsql = "SELECT m.Type, b.PartNumber, m.ModelName, m.RotorBush, " +
                                "m.ShaftPartnum, m.ShaftBushAssyPartnum, " +
                                "b.Racks, b.Quantity, i.Sample_img " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation b " +
                            "ON m.PartNumber = b.PartNumber " +
                            "LEFT JOIN Parts_MoldingRawImage i ON i.PartNumber = m.PartNumber " +
                            "WHERE m.PartNumber = '" + Partnum + "'";
            return db.GetData(searchsql);
        }

        public  DataTable getProductList()
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

        public  DataTable getMoldingRowList()
        {
            GlobalDb db = new GlobalDb();
            var strsql = "SELECT " +
                           "m.PartNumber, m.ModelName, m.RotorBush, " +
                           "m.ShaftPartnum, m.ShaftBushAssyPartnum, m.type, " +
                           "p.Racks, p.Quantity " +
                        "FROM Part_MoldingBushParts m " +
                        "INNER JOIN Part_ProductBushLocation p  " +
                        "ON m.PartNumber = p.PartNumber " +
                        "WHERE m.PartNumber = '" + Partnum + "'";

            return db.GetData(strsql);
        }

        public  DataTable getModelingRowByType(int bushtype)
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

            string strsql = "SELECT m.PartNumber, p.Racks, p.Quantity, m.Type " +
                            "" + filterstr + " " +
                            "FROM Part_MoldingBushParts m " +
                            "INNER JOIN Part_ProductBushLocation p " +
                            "ON m.PartNumber = p.PartNumber " +
                            "WHERE m.Type = " + bushtype + " ";

            DataTable td = db.GetData(strsql);
            return td;
        }


        public bool EditMasterlist(string partnum, int qty, int type)
        {
            GlobalDb db = new GlobalDb();

            //UPDATES THE STORAGE QUANTITY BY PALLETE
            string updatestorage = "UPDATE Part_ProductBushLocation SET " +
                                   " Quantity =@Quantity " +
                                   " WHERE PartNumber = @PartNumber AND Racks =@Racks";
            SqlParameter[] updateparamaters =
            {
                   new SqlParameter("@Quantity", qty),
                   new SqlParameter("@PartNumber", partnum),
                   new SqlParameter("@Racks", type)
            };


            bool result = db.ExecuteCommandUpdate(updatestorage, updateparamaters);

            return result;
        }


    }
}
