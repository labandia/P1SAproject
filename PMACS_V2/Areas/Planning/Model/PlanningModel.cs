using System;


namespace PMACS_V2.Areas.Planning.Model
{
    public class Planningmodel
    {
        public int RecordId { get; set; }
        public string DateUpload { get; set; }
        public string DOI_Date { get; set; }
        public string Date_Created { get; set; }
        public string Branch { get; set; }
        public string MD_Request { get; set; }
        public string SDP_Shoporder { get; set; }
        public string SDP_Sales_Partnum { get; set; }
        public int SDP_Sales_Number { get; set; }
        public string Sales_Request_Date { get; set; }
        public string Previous_Update { get; set; }
        public string PC_Proposed_Date { get; set; }
        public string M1_Latest_Update { get; set; }
        public string PC_Latest_Proposed_Date { get; set; }
        public string Status { get; set; }
        public string Partnumber { get; set; }
        public string Partname { get; set; }
        public decimal Usage { get; set; }
        public string Judgement { get; set; }
    }

    public class ProductsModel
    {
        public string Partnumber { get; set; }
        public string Partname { get; set; }
        public int  Totalpart { get; set; }
    }

    public class SalesMonthInfo
    {
        public string MonthAbbreviation { get; set; }
        public string Year { get; set; }
    }

    public class ProductSummaryModel
    {
        public DateTime ReferenceDate { get; set; }
        public string Partnum { get; set; }
        public string Partname { get; set; }
        public int TotalQuan { get; set; }
        public int ImportID { get; set; }
        public int Addition { get; set; }
    }

    public class LackingModel
    {
        public string Dateinput { get; set; }
        public int Gtotal { get; set; }
    }

    public class EndMonthModel
    {
        public int RecordID { get; set; }
        public string DateMonth { get; set; }
        public double EndTotalOrders { get; set; }
        public double EndRemainOrders { get; set; }
        public decimal EndImprovements { get; set; }
        public double CurrentTotalOrders { get; set; }
        public double CurrentRemains { get; set; }
        public decimal CurrentImprovements { get; set; }
        public string EndYear { get; set; }
    }

    public class ShopOrderModel
    {
        public int ShopCount { get; set; }
    }

    public class DateModel
    {
        public string FirstDate { get; set; }
        public string LastDate { get; set; }
    }

    public class ShopOrderResultModel
    {
        public string Branch { get; set; }
        public string SDP_Shoporder { get; set; }
        public string SDP_Sales_Partnum { get; set; }
        public int SDP_Sales_Number { get; set; }
        public string Sales_Request_Date { get; set; }
        public string PC_Proposed_Date { get; set; }
        //public int ImportsID { get; set; }
    }

    public class CustomDate
    {
        public int Month { get; set; }
        public int Day { get; set; }
        public int Year { get; set; }
        public int Count { get; set; }
    }

    public class RequestTotalRowModel
    {
        public int MonthUpload { get; set; }
        public int Totalrow { get; set; }
    }

    public class BranchModel
    {
        public int Branchcount { get; set; }
        public string Branch { get; set; }
    }

    public class M1ExcelData
    {
        public DateTime DateUpload { get; set; }
        public int No { get; set; }
        public string DOItoM1 { get; set; }
        public string CreatedDate { get; set; }
        public string MDBasedOnSalesRequest { get; set; }
        public string Branch { get; set; }
        public string SDPOrderNo { get; set; }
        public string SDPSalesPartNo { get; set; }
        public int SDPSalesQty { get; set; }
        public DateTime SalesRequestedShipDate { get; set; }
        public string PreviousUpdate { get; set; }
        public string PC1ProposedShipDate { get; set; }
        public string M1LatestUpdate { get; set; }
        public string PC1LatestProposedShipDate { get; set; }
        public int Negativenum { get; set; }
        public string AdjustDueDate { get; set; }
        public string ReplyStatus { get; set; }
        public int Days { get; set; }
        public string MDDate { get; set; }
        public int LeadTime { get; set; }
        public string PartNo { get; set; }
        public string PartName { get; set; }
        public double Usage { get; set; }
        public double NeedQty { get; set; }
        public string Judgement { get; set; }
    }

    public class MonthlySales
    {
        public int TotalOrderCount { get; set; }
        public int MonthUpload { get; set; }
        public int DateYearupload { get; set; }
    }






    public class PostMontlyEndResult
    {
        public int RecordID { get; set; }
        public double EndTotalOrdersEdit { get; set; }
        public double EndRemainOrdersEdit { get; set; }
        public decimal CurrentTotalOrdersEdit { get; set; }
        public double CurrentRemainsEdit { get; set; }
    }
}