using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parts_locator.Models
{
    internal class Transaction_Bush : BushProducts
    {
        GlobalDb con;

        private string _Shoporder;
        private string _Input;
        private int _CurrentQuan;
        private int _NewQuan;
        private int _Action;

        public string ShopOrder { get { return _Shoporder; } set { _Shoporder = value; } }
        public int CurrentQuantity { get { return _CurrentQuan; } set { _CurrentQuan = value; } }
        public int newQuantity { get { return _NewQuan; } set { _NewQuan = value; } }
        public string Inputby { get { return _Input; } set { _Input = value; } }
        public int Action { get { return _Action; } set { _Action = value; } }


       

        // GET THE SUMMARY MONITORING IN AND OUT 
        public DataTable GetMoldingShoporder(int act)
        {
            con = new GlobalDb();
            string strsql = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, " +
                            "FORMAT(DateInput, 'HH:mm:ss tt') as TimeInput,PartNumber, " +
                            "Quantity,Inputby " +
                            "FROM Part_transaction_BushMold_shoporder " +
                            "WHERE Action = " + act + "";
            DataTable dt = con.GetData(strsql);
            return dt;
        }
    }
}
