using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.Final.Model
{
    public class FanTraceabilityManufacturingOrder
    {
        public int RecordID { get; set; }
        public string Line { get; set; } = string.Empty;
        public string FinalShopOrder { get; set; } = string.Empty;
        public string ItemNo { get; set; }
        public string Model { get; set; } = string.Empty;
        public string WC { get; set; } = string.Empty;
        public int PlanQty { get; set; } = 0;
        public DateTime? PlanStartDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public string Note { get; set; }
        public DateTime? FinalFinishedDate { get; set; }
        public string FAStatus { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string ShipmentMode { get; set; }
        public bool WithSR { get; set; } = false;
        public string OrderRemarks { get; set; }
        public int OrderStatus { get; set; }

        public string CompletedSection { get; set; }

        public string Molding { get; set; }
        public string Press { get; set; }
        public string Circuit { get; set; }
        public string Rotor { get; set; }
        public string Winding { get; set; }
        public string final { get; set; }
        public string NextItem { get; set; }
        public int InputQty { get; set; }
    }


    public class AssemblyRecord
    {
        public string FinalShopOrder { get; set; }
        public string Line { get; set; }
        public int Status { get; set; }
        public string OnProcessModel { get; set; }
        public string NextProcessModel { get; set; }
        public string SpecialProcess { get; set; }
        public string Remarks { get; set; }
        public string NextItem { get; set; }

        public string LineStatus
        {
            get
            {
                string statusText = Status == 2 ? "Online" : "Offline";
                return $"{Line}\n{statusText}";
            }
        }
    }


    public class ProductionRecord
    {
        public int Id { get; set; }

        public string Line { get; set; } = string.Empty;

        private string _shopOrder = string.Empty;
        public string ShopOrder
        {
            get => _shopOrder;
            set => _shopOrder = (value ?? string.Empty).Replace(" ", "");
        }

        private string _partNo = string.Empty;
        public string PartNo
        {
            get => _partNo;
            set => _partNo = (value ?? string.Empty).Replace(" ", "");
        }

        private string _model = string.Empty;
        public string Model
        {
            get => _model;
            set => _model = (value ?? string.Empty).Replace(" ", "");
        }

        public string WC { get; set; } = string.Empty;
        public int Qty { get; set; }
        public string PlanStart { get; set; } = string.Empty;
        public string DispatchDate { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string IfsFinish { get; set; } = string.Empty;
        public string FaStatus { get; set; } = string.Empty;
        public string Shipment { get; set; } = string.Empty;
        public string Mode { get; set; } = string.Empty;
        public bool WithSr { get; set; } = false;
        public string Remarks { get; set; } = string.Empty;

        // ── Upload tracking (not persisted) ───────────────────────────────────
        public int RowNumber { get; set; }
        public string Status { get; set; } = "Pending";
        public string ErrorMessage { get; set; } = string.Empty;
    }

    public class UploadProductionRecord
    {
        public int RecordID { get; set; }
        public string Line { get; set; } = string.Empty;
        public string FinalShopOrder { get; set; } = string.Empty;
        public string ItemNo { get; set; }
        public string Model { get; set; } = string.Empty;
        public string WC { get; set; } = string.Empty;
        public int PlanQty { get; set; } = 0;
        public int UploadPlanQty { get; set; }
        public int OrderPlanQty { get; set; }

        public string UploadPlanStartDate { get; set; } = string.Empty;
        public string OrderPlanStartDate { get; set; } = string.Empty;
        public string StatusCheck { get; set; } = string.Empty;
        public bool IsApproved { get; set; } = false;
    }





    public class UploadDataModel
    {
        public int RecordID { get; set; }
        public string Line { get; set; } = string.Empty;
        public string FinalShopOrder { get; set; } = string.Empty;
        public string ItemNo { get; set; }
        public string Model { get; set; } = string.Empty;
        public string WC { get; set; } = string.Empty;
        public int PlanQty { get; set; } = 0;
        public DateTime? PlanStartDate { get; set; }
        public string DispatchDate { get; set; }
        public string Note { get; set; }
        public DateTime? FinalFinishedDate { get; set; }
        public string FAStatus { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string ShipmentMode { get; set; }
        public bool WithSR { get; set; } = false;
        public string OrderRemarks { get; set; }
        public int OrderStatus { get; set; }
        public string StatusCheck { get; set; }
        public bool IsApproved { get; set; }

    }

    public class UploadRowDto
    {
        public string ShopOrder { get; set; }
        public string PartNo { get; set; }
        public string Model { get; set; }
        public string Wc { get; set; }
        public int Qty { get; set; }
        public string PlanStart { get; set; }
        public RowResult Result { get; set; }
    }

    public class RowResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }




    public class UploadJobState
    {
        public string Status { get; set; }
        public int Total { get; set; }
        public int Current { get; set; }
        public int Success { get; set; }
        public int Failed { get; set; }
        public int LastSent { get; set; }
        public string Message { get; set; }
        public List<UploadRowResult> Rows { get; set; }
    }

    public class UploadRowResult
    {
        public string ShopOrder { get; set; }
        public string PartNo { get; set; }
        public string Model { get; set; }
        public string Wc { get; set; }
        public string PlanStart { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}