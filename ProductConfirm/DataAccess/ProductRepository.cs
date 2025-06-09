using ProductConfirm.Global;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductConfirm.Data
{
    public class ProductRepository : IProductRepository
    {
        // RETRIEVAL OF DATA FOR PRODUCT MASTERLIST
        public async Task<DataTable> GetAllProducts()
        {
            Dataconnect db = new Dataconnect();
            string strsql = "SELECT " +
                               "r.RotorProductID, r.RotorAssy, r.ProductType, " +
                               "r.MachinePressureMinMax, r.RecommendedPressureSetting, " +
                               "p.CaulkingDentMin, p.CaulkingDentMax, p.ShaftLengthMin, " +
                               "p.ShaftLengthMax, p.SEA_Min, p.SEA_Max, p.ShaftPullingForce, " +
                               "p.BushPullingForce " +
                            "FROM ProdCon_RotorProduct r " +
                            "INNER JOIN  ProdCon_RotorProductInfo p " +
                            "ON r.RotorProductID = p.RotorProductID";

            return await db.GetData(strsql);
        }

        public async Task<DataTable> GetOneProduct(int ID)
        {
            Dataconnect db = new Dataconnect();
            string strsql = "SELECT " +
                               "r.RotorProductID, r.RotorAssy, r.ProductType, " +
                               "r.MachinePressureMinMax, r.RecommendedPressureSetting, " +
                               "p.CaulkingDentMin, p.CaulkingDentMax, p.ShaftLengthMin, " +
                               "p.ShaftLengthMax, p.SEA_Min, p.SEA_Max, p.ShaftPullingForce, " +
                               "p.BushPullingForce, p.MagnetHeightMin, p.MagnetHeightMax, r.ModelType " +
                            "FROM ProdCon_RotorProduct r " +
                            "INNER JOIN  ProdCon_RotorProductInfo p " +
                            "ON r.RotorProductID = p.RotorProductID " +
                            "WHERE r.RotorProductID = " + ID + "";

            return await db.GetData(strsql);
        }


        public async Task<DataTable> GetMinandMaxProduct(int rotorid)
        {
            Dataconnect db = new Dataconnect();

            string strsql = "SELECT r.ProductType, p.CaulkingDentMin as Min,  p.CaulkingDentMax as Max " +
                            "FROM ProdCon_RotorProduct r " +
                            "INNER JOIN ProdCon_RotorProductInfo p " +
                            "ON r.RotorProductID = p.RotorProductID " +
                            "WHERE r.RotorProductID = " +  rotorid + "";
            return  await db.GetData(strsql);
        }


        // RETRIEVAL OF DATA FOR SHOPORDERS 
        public async Task<DataTable> GetDataAndExportoExcel()
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
        public async Task<DataTable> GetShoporderDetails(string shoporder, int ID)
        {
            Dataconnect db = new Dataconnect();
            string strsql;
            strsql = "SELECT i.Tool_name as Measurements, so.Status, so.ShopOrderID as ShopProdID " +
                  "FROM ProdCon_item_tbl i " +
                  "LEFT JOIN ProdCon_ShopOrderData_tbl so ON i.Item_ID  = so.Item_ID " +
                  "AND so.Shoporder = '" + shoporder + "' AND so.ShopOrderID = " + ID + "";
            DataTable dt = await db.GetData(strsql);
            return dt;
        }
        public async Task<DataTable> GetShoporderlist()
        {
           Dataconnect db = new Dataconnect();
           string strsql = "SELECT ShoporderID,  FORMAT(s.Date_input, 'MM/dd/yyyy') as Date_input,  s.Shoporder, " +
                           "p.RotorAssy,p.ProductType, s.Shift, " +
                           "s.Line, s.Inputby, p.MachinePressureMinMax, p.RotorProductID " +
                           "FROM  ProdCon_ShopOrder_tbl s " +
                           "INNER JOIN  ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID";
            return await db.GetData(strsql);
        }
        public async Task<DataTable> GetSummaryDataConfirmation()
        {
            Dataconnect db = new Dataconnect();
            string strsql;
            strsql = "SELECT  s.Date_input, s.Shift, s.Line, s.Shoporder, p.MachinePressureMinMax as Max, " +
                     "sp.SL_supply, sp.SL_lot, a.SL_first, a.SL_second, a.SL_third, a.SL_fourth, a.SL_fifth, " +
                     "sp.SE_supply, sp.SE_lot, a.SE_first, a.SE_second, a.SE_third, a.SE_fourth, a.SE_fifth, " +
                     "sp.CD_supply, sp.CD_lot, a.CD_first, a.CD_second, a.CD_third, a.CD_fourth, a.CD_fifth, a.CD_fifth as six, a.CD_fifth as seven, a.CD_fifth as eight, " +
                     "sp.SP_supply, sp.SP_lot, a.SP_first, a.SP_second, a.SP_third, a.SP_fourth, a.SP_fifth, " +
                     "sp.BP_supply, sp.BP_lot, a.BP_first, a.BP_second, a.BP_third, a.BP_fourth, a.BP_fifth, " +
                     "sp.MH_supply, sp.MH_supply, a.MH_first_min, a.MH_second_min, a.MH_third_min, a.MH_fourth_min, a.MH_fifth_min, " +
                     "a.MH_first_max, a.MH_second_max, a.MH_third_max, a.MH_fourth_max, a.MH_fifth_max, s.Inputby " +
                     "FROM ProdCon_ShopOrder_tbl s " +
                     "INNER JOIN ProdCon_RotorProduct p ON p.RotorProductID = s.RotorProductID " +
                     "INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = s.ShopOrderID " +
                     "INNER JOIN ProdCon_ShopOrder_ActualData a ON a.ShopOrderID = s.ShopOrderID";
            return await db.GetData(strsql);
        }
    }

    
}
