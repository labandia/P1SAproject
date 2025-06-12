using ProductConfirm.Global;
using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;


namespace ProductConfirm.Data
{
    internal class Products
    {
        public int Id { get; set; }
        public string Partnum {  get; set; }
        public string Model_name { get; set; }
        public string Machinepressure {  get; set; }
        public decimal Caulkmin { get; set; }
        public decimal Caulkmax { get; set; }
        public decimal shaftmin { get; set; }
        public decimal Shaftmax { get; set; }
        public decimal ShaftEdgemin { get; set; }
        public decimal ShaftEdgemax { get; set; }
        public decimal ShaftPull { get; set; }
        public decimal BushPull { get; set; }



        //DISPLAY THE MASTERLIST 
        public  async Task<DataTable> getProductList(string search)
        {
            Dataconnect db = new Dataconnect();
         
           

            string strsql = "SELECT " +
                                "r.RotorProductID, r.RotorAssy, r.ProductType, " +
	                            "r.MachinePressureMinMax, " + 
	                            "CASE WHEN p.CaulkingDentMin IS NOT NULL AND p.CaulkingDentMax IS NOT NULL " +
                               "THEN CONCAT(CAST(p.CaulkingDentMin AS VARCHAR(10)), ' - ', CAST(p.CaulkingDentMax AS VARCHAR(10))) " +
                               "ELSE '-' END AS CaulkingDentMinMax, " +
                               "p.CaulkingDentMax as CaulkingDentTarget, " +
                               "CASE WHEN p.ShaftLengthMin IS NOT NULL AND p.ShaftLengthMax IS NOT NULL " +
                               "THEN CONCAT(CAST(p.ShaftLengthMin AS VARCHAR(10)), ' - ', CAST(p.ShaftLengthMax AS VARCHAR(10))) " +
                               "ELSE '-' END AS ShaftLengthMinMax, " +

                               "CASE WHEN p.SEA_Min IS NOT NULL AND p.SEA_Max IS NOT NULL " +
                               "THEN CONCAT(CAST(p.SEA_Min AS VARCHAR(10)), ' - ', CAST(p.SEA_Max AS VARCHAR(10)))  " + 
                               "ELSE '-' END AS SEA_MinMax, " +

                               "CASE WHEN p.ShaftPullingForce IS NOT NULL THEN " +
                               "CONCAT(CAST(p.ShaftPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                               "ELSE '-' END AS ShaftPullingForce, " +

                              "CASE WHEN p.BushPullingForce IS NOT NULL THEN " +
                                   "CONCAT(CAST(p.BushPullingForce AS VARCHAR(10)), ' Kgf or more ') " +
                              "ELSE '-'  END AS BushPullingForce,  " +
                              "CASE WHEN p.MagnetHeightMin IS NOT NULL AND p.MagnetHeightMax IS NOT NULL " +
                              "THEN CONCAT(CAST(p.MagnetHeightMin AS VARCHAR(10)), ' - ', CAST(p.MagnetHeightMax AS VARCHAR(10))) " +
                              "ELSE '-' END AS MagnetHeightMinMax " +
                              "FROM ProdCon_RotorProduct r " +
                              "INNER JOIN  ProdCon_RotorProductInfo p " +
                              "ON r.RotorProductID = p.RotorProductID " +
                              "WHERE  r.RotorAssy LIKE '%" + search + "%' " + 
                              "ORDER BY p.RotorProductID ASC";

            return await db.GetData(strsql); 
        }
        
        public async Task<DataTable> getOneproduct(string part, string partname)
        {
            Dataconnect db = new Dataconnect();

            string strsql = "SELECT RotorProductID " +
                          "FROM ProdCon_RotorProduct " +
                          "WHERE RotorAssy = '" + part + "' AND  ProductType = '" + partname + "'";
            return await db.GetData(strsql);
        }

        public static async Task<DataTable> GetMinandMax(int RotorId)
        {
            Dataconnect db = new Dataconnect();

            string strsql = "SELECT r.ProductType, " +
	                            "p.CaulkingDentMin,  p.CaulkingDentMax, " +
	                            "p.ShaftLengthMin, p.ShaftLengthMax, " +
	                            "p.SEA_Min, p.SEA_Max, " +
	                            "p.ShaftPullingForce, p.BushPullingForce, " +
                                "p.MagnetHeightMin, p.MagnetHeightMax " +
                            "FROM ProdCon_RotorProduct r  " +
                            "INNER JOIN ProdCon_RotorProductInfo p  " +
                            "ON r.RotorProductID = p.RotorProductID  " +
                            "WHERE r.RotorProductID = " +  RotorId + "";
            return await db.GetData(strsql);     
        }

        public  async Task<DataTable> GetProductID()
        {
            Dataconnect db = new Dataconnect();

            string strsql = "SELECT TOP(1) RotorProductID FROM ProdCon_RotorProduct " +
                            "ORDER BY RotorProductID DESC";
            
            return await db.GetData(strsql);
        }



        // RETRIEVAL OF DATA FOR SHOPORDERS 
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
        public static async Task<DataTable> GetShoporderDetails(string shoporder, int ID)
        {
            Dataconnect db = new Dataconnect();
            string strsql = "SELECT i.Tool_name as Measurements, so.Status, so.ShopOrderID as ShopProdID " +
                  "FROM ProdCon_item_tbl i " +
                  "LEFT JOIN ProdCon_ShopOrderData_tbl so ON i.Item_ID  = so.Item_ID " +
                  "AND  so.ShopOrderID = " + ID + "";
            return await db.GetData(strsql);
        }
        public static async Task<DataTable> GetShoporderlist()
        {
            Dataconnect db = new Dataconnect();
            string strsql = "SELECT s.ShoporderID, FORMAT(s.Date_input, 'MM/dd/yy') as Date_input,  FORMAT(s.Date_input, 'hh:mm:ss:tt') as Date_time,  s.Shoporder, " +
                            "p.RotorAssy,p.ProductType, s.Shift, " +
                            "s.Line, s.Inputby, p.MachinePressureMinMax, p.RotorProductID, " +
                            "s.Remarks, s.ConfirmBy, s.Stats " +
                            "FROM  ProdCon_ShopOrder_tbl s " +
                            "INNER JOIN  ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID " +
                            "ORDER BY s.Date_input DESC";
            return await db.GetData(strsql);
        }


        public static async Task<DataTable> GetSummaryDataConfirmation(string search)
        {
            Dataconnect db = new Dataconnect();
            string strfilter = String.IsNullOrWhiteSpace(search) ? "" : "WHERE s.Shoporder LIKE '%" + search + "%' ";

            string strsql = "SELECT  FORMAT(s.Date_input, 'MM/dd/yyyy') as Date_input, s.Shift, s.Line, s.Shoporder, p.MachinePressureMinMax as Max, " +
                     "sp.SL_supply, sp.SL_lot, a.SL_first, a.SL_second, a.SL_third, a.SL_fourth, a.SL_fifth, " +
                     "sp.SE_supply, sp.SE_lot, a.SE_first, a.SE_second, a.SE_third, a.SE_fourth, a.SE_fifth, " +
                     "sp.CD_supply, sp.CD_lot, a.CD_first, a.CD_second, a.CD_third, a.CD_fourth, a.CD_fifth, a.CD_fifth as six, a.CD_fifth as seven, a.CD_fifth as eight, " +
                     "sp.SP_supply, sp.SP_lot, a.SP_first, a.SP_second, a.SP_third, a.SP_fourth, a.SP_fifth, " +
                     "sp.BP_supply, sp.BP_lot, a.BP_first, a.BP_second, a.BP_third, a.BP_fourth, a.BP_fifth, " +
                     "sp.MH_supply, sp.MH_supply, a.MH_first_min, a.MH_second_min, a.MH_third_min, a.MH_fourth_min, a.MH_fifth_min, " +
                     "a.MH_first_max, a.MH_second_max, a.MH_third_max, a.MH_fourth_max, a.MH_fifth_max, s.Inputby," +
                     "s.ConfirmBy, s.Remarks " +
                     "FROM ProdCon_ShopOrder_tbl s " +
                     "INNER JOIN ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID " +
                     "LEFT JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID " +
                     "LEFT JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID " +
                     strfilter +
                     "ORDER BY s.Date_input DESC";
            return await db.GetData(strsql);
        }
    }

}
