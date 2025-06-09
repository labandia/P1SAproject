using ProductConfirm.Global;
using ProductConfirm.Modals;
using System.Collections.Generic;
using System;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace ProductConfirm.Data
{
    public class Shopordersdata
    {
        private readonly Dataconnect db;
        public string strsql = "";

        public string Date_Input { get; set; }
        public string Shift { get; set; }
        public string Line { get; set; }
        public string ShopOrder { get; set; }
        public string Max { get; set; }
        public string SL_Supply { get; set; }
        public string SL_Lot { get; set; }
        public string SL_First { get; set; }
        public string SL_Second { get; set; }
        public string SL_Third { get; set; }
        public string SL_Fourth { get; set; }
        public string SL_Fifth { get; set; }
        public string SE_Supply { get; set; }
        public string SE_Lot { get; set; }
        public string SE_First { get; set; }
        public string SE_Second { get; set; }
        public string SE_Third { get; set; }
        public string SE_Fourth { get; set; }
        public string SE_Fifth { get; set; }
        public string CD_Supply { get; set; }
        public string CD_Lot { get; set; }
        public string CD_First { get; set; }
        public string CD_Second { get; set; }
        public string CD_Third { get; set; }
        public string CD_Fourth { get; set; }
        public string CD_Fifth { get; set; }
        public string Six { get; set; }
        public string Seven { get; set; }
        public string Eight { get; set; }
        public string SP_Supply { get; set; }
        public string SP_Lot { get; set; }
        public string SP_First { get; set; }
        public string SP_Second { get; set; }
        public string SP_Third { get; set; }
        public string SP_Fourth { get; set; }
        public string SP_Fifth { get; set; }
        public string BP_Supply { get; set; }
        public string BP_Lot { get; set; }
        public string BP_First { get; set; }
        public string BP_Second { get; set; }
        public string BP_Third { get; set; }
        public string BP_Fourth { get; set; }
        public string BP_Fifth { get; set; }
        public string MH_Supply { get; set; }
        public string MH_First_Min { get; set; }
        public string MH_Second_Min { get; set; }
        public string MH_Third_Min { get; set; }
        public string MH_Fourth_Min { get; set; }
        public string MH_Fifth_Min { get; set; }
        public string MH_First_Max { get; set; }
        public string MH_Second_Max { get; set; }
        public string MH_Third_Max { get; set; }
        public string MH_Fourth_Max { get; set; }
        public string MH_Fifth_Max { get; set; }
        public string InputBy { get; set; }
        public string ConfirmBy { get; set; }
        public string Remarks { get; set; }



        public Shopordersdata() {
           db = new Dataconnect();
        }

        public async Task<DataTable> getShoporderlist()
        {
            
            strsql = "SELECT ShoporderID,  FORMAT(s.Date_input, 'MM/dd/yyyy') as Date_input,  s.Shoporder, " +
                            "p.RotorAssy,p.ProductType, s.Shift, " +
                            "s.Line, s.Inputby, p.MachinePressureMinMax, p.RotorProductID " +
                            "FROM  ProdCon_ShopOrder_tbl s " +
                            "INNER JOIN  ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID" +
                            "ORDER BY s.RotorProductID DESC";
            return await db.GetData(strsql);
        }

        public static async Task<DataTable> getShoporderDetails(string shoporder, int ID)
        {
            Dataconnect db = new Dataconnect();
            string strsql = "SELECT  i.Tool_name as Measurements, s.Status, s.ShopOrderID as ShopProdID " +
                            "FROM ProdCon_ShopOrderData_tbl s " +
                            "INNER JOIN ProdCon_item_tbl i on i.Item_ID = s.Item_ID " +
                            "AND s.Shoporder = '" + shoporder + "' AND s.ShopOrderID = " + ID + "";
            DataTable dt = await db.GetData(strsql);



            return dt;
        }


        public static async Task<DataTable> getSummaryDataConfirmation()
        {
            Dataconnect db = new Dataconnect();
            string strsql;
            strsql = "SELECT  FORMAT(s.Date_input, 'dd/MM/yyyy') as Date_input, s.Shift, s.Line, s.Shoporder, p.MachinePressureMinMax as Max, " +
                     "sp.SL_supply, sp.SL_lot, a.SL_first, a.SL_second, a.SL_third, a.SL_fourth, a.SL_fifth, " +
                     "sp.SE_supply, sp.SE_lot, a.SE_first, a.SE_second, a.SE_third, a.SE_fourth, a.SE_fifth, " +
                     "sp.CD_supply, sp.CD_lot, a.CD_first, a.CD_second, a.CD_third, a.CD_fourth, a.CD_fifth, a.CD_fifth as six, a.CD_fifth as seven, a.CD_fifth as eight, " +
                     "sp.SP_supply, sp.SP_lot, a.SP_first, a.SP_second, a.SP_third, a.SP_fourth, a.SP_fifth, " +
                     "sp.BP_supply, sp.BP_lot, a.BP_first, a.BP_second, a.BP_third, a.BP_fourth, a.BP_fifth, " +
                     "sp.MH_supply, sp.MH_lot, a.MH_first_min, a.MH_second_min, a.MH_third_min, a.MH_fourth_min, a.MH_fifth_min, " +
                     "a.MH_first_max, a.MH_second_max, a.MH_third_max, a.MH_fourth_max, a.MH_fifth_max, s.Inputby " +
                     "FROM ProdCon_ShopOrder_tbl s " +
                     "INNER JOIN ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID " +
                     "INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID " +
                     "INNER JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID";
            return await db.GetData(strsql);
        }


        public static async Task<DataTable> GetDataAndExportoExcel()
        {
            Dataconnect db = new Dataconnect();
            string strsql;
            strsql = "SELECT r.RotorProductID, " +
                        "r.RotorAssy, r.ProductType, r.MachinePressureMinMax, r.RecommendedPressureSetting, " +
                        "CASE " +
                           "WHEN i.CaulkingDentMin IS NOT NULL AND i.CaulkingDentMax IS NOT NULL " +
                           "THEN CONCAT(CAST(i.CaulkingDentMin AS VARCHAR(10)), ' - ', CAST(i.CaulkingDentMax AS VARCHAR(10))) " +
                           "ELSE '-' " +
                        "END AS CaulkingDent, " +
                        "CONCAT(CAST(i.SEA_Min AS VARCHAR(10)), ' - ', CAST(i.SEA_Max AS VARCHAR(10)), ' mm') as SurfaceEdge, " +
                        "CONCAT(CAST(i.ShaftLengthMin AS VARCHAR(10)), ' - ', CAST(i.ShaftLengthMax AS VARCHAR(10)), ' mm') as ShaftLength, " +
                        "CASE " +
                           "WHEN i.ShaftPullingForce IS NOT NULL " +
                           "THEN CONCAT(CAST(i.ShaftPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                           "ELSE '-' " +
                        "END AS ShaftPullingForce, " +
                        "CASE " +
                           "WHEN i.BushPullingForce IS NOT NULL " +
                           "THEN CONCAT(CAST(i.BushPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                           "ELSE '-' " +
                        "END AS BushPullingForce " +
                        "FROM ProdCon_RotorProduct r " +
                        "INNER JOIN ProdCon_RotorProductInfo i ON i.RotorProductID = r.RotorProductID " +
                        "WHERE r.RotorAssy = '00600031-01' AND r.ProductType = '9BAM cannon'";
           return await db.GetData(strsql);
        }


        

    }
}
