using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssuanceSystem.Model
{
    public class IssuanceModel
    {
        public string Partnumber { get; set; }
        public string Application { get; set; }
        public string Revision { get; set; }
        public string Remarks { get; set; }
    }

    public class IssuanceTransactionModel
    {
        public int TransactionID { get; set; }
        public string DateInput { get; set; }
        public string FinalShopOrder { get; set; }
        public string PCBpartnumber { get; set; }
        public string PCBCode { get; set; }
        public string ApplicationSet { get; set; }  
        public string Remarks { get; set; }
    }
}
