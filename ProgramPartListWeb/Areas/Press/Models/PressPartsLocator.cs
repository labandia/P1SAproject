using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Models
{
    public class PressMasterlistModel
    {
        public int Storage_ID { get; set; }
        public string Partnum { get; set; }
        public string Model { get; set; }
        public int Racksnum { get; set; }
        public int Levelnum { get; set; }
        public string Postnum { get; set; }
        public int Boxnum { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public int Master_ID {  get; set; }
    }
    public class AddPressMasterlistModel
    {
        public string Partnum { get; set; }
        public string Model { get; set; }
        public int Racksnum { get; set; }
        public int Levelnum { get; set; }
        public string Postnum { get; set; }
        public int Boxnum { get; set; }
        public int NoteID { get; set; }
        public int Quantity { get; set; }
    }
    public class PressTransactHistoryModel
    {
        public string DateInput { get; set; }
        public string TimeInput { get; set; }
        public string ShopOrder { get; set; }
        public string Partnum { get; set; }
        public string Model { get; set; }
        public int Racksnum { get; set; }
        public int Levelnum { get; set; }
        public int Boxnum { get; set; }
        public string Postnum { get; set; }
        public string InputQuan { get; set; }
        public string InputBy { get; set; }
    }

    public class IssuanceModel
    {
        public int IssuanceID { get; set; }
        public string DateInput { get; set; }
        public string FA_Shoporder { get; set; }
        public string FA_Plan { get; set; }
        public string Partnum { get; set; }
        public string Model { get; set; }
        public int Racknum { get; set; }
        public int levelnum { get; set; }
        public int boxnum { get; set; }
        public string postnum { get; set; }
        public string TimeReceived { get; set; }
        public string Received { get; set; }
        public string DateIssuance { get; set; }
        public string TimeIssuance { get; set; }
        public int IssuedQuan { get; set; }
        public string IssuedBy { get; set; }
        public int Stats { get; set; }
    }



    public class PressIDNote { 
       public int NoteID { get; set; }
       public string Color { get; set; }
    }
}