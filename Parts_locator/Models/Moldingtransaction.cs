using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Models
{
    internal class Moldingtransaction
    {
        GlobalDb con;
        public string ShopOrder { get; set; }
        public string PartNumber { get; set; }
        public int CurrentQuantity { get; set; }
        public int newQuantity { get; set; }
        public string Inputby { get; set; }
        public int Action { get; set; }


        public Moldingtransaction()
        {
            this.ShopOrder = ShopOrder;
            this.PartNumber = PartNumber;
            this.CurrentQuantity = CurrentQuantity;
            this.newQuantity = newQuantity;
            this.Inputby = Inputby;
            this.Action = Action;
        }

        // GET THE SUMMARY MONITORING IN AND OUT 
        public DataTable GetMoldingShoporder(int act)
        {
            con = new GlobalDb();
            string strsql = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, " +
                            "FORMAT(DateInput, 'HH:mm:ss tt') as TimeInput, " +
                            "ShopOrder,PartNumber, " +
                            "Quantity,Inputby " +
                            "FROM Part_transaction_BushMold_shoporder " +
                            "WHERE Action = " + act + "";
            DataTable  dt = con.GetData(strsql);
            return dt;
        }
    }
}
