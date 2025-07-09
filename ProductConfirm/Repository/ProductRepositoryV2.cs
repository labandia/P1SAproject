using ProductConfirm.Data;
using ProductConfirm.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductConfirm.DataAccess
{
    public class ProductRepositoryV2 : IProductRepositoryV2
    {
  
        public async Task<List<MasterlistModel>> GetMasterlist()
        {
            string strsql = "SELECT RotorProductID, RotorAssy, ProductType, MachinePressureMinMax, " + 
                            "RecommendedPressureSetting, ModelType " +
                            "FROM ProdCon_RotorProduct";
            return await SqlDataAccess.GetData<MasterlistModel>(strsql);
        }
        public Task<List<ProductModel>> GetAllProducts() => SqlDataAccess.GetData<ProductModel>("ProdMasterlist");
        public Task<List<ExportModel>> GetDataAndExportoExcel() => SqlDataAccess.GetData<ExportModel>("ProdMasterlist");

        public Task<List<ProductToolsModel>> GetProductsTools(string shoporder, int ID)
        {
            string strsql = "Measurements";
            var parameters = new { Shoporder = shoporder, ShopOrderID = ID };
            return SqlDataAccess.GetData<ProductToolsModel>(strsql, parameters);
        }

        public async Task<List<ShopOrderModel>> GetShoporderlist(int CurrentPageIndex, int pageSize)
        {
            return await SqlDataAccess.GetData<ShopOrderModel>("ShopOrderlist", new { PageNumber = CurrentPageIndex, PageSize = pageSize });
        }  
        public async Task<List<SummaryProductModel>> GetSummaryDataConfirmation()
        {
            return await SqlDataAccess.GetData<SummaryProductModel>("SummaryComfirmation");
        }
    
        // CRUD OPERATION PROCESS
        public async Task<bool> AddProducts(AddProductDetailsModel prod)
        {
            bool isSuccess = await SqlDataAccess.UpdateInsertQuery("InsertProduct", prod);
            // INSERT THE PRODUCT MAIN 
            if (isSuccess)
            {
                // GET THE LAST ID NUMBER OF THE TABLE
                var sortedProducts = await GetMasterlist();
                IEnumerable<int> sortedProductIDs = sortedProducts
                                .OrderByDescending(p => p.RotorProductID)
                                .Select(p => p.RotorProductID);
                int rotorid = sortedProductIDs.FirstOrDefault();

                var infoparams = new
                {
                    RotorProductID = rotorid,
                    CaulkingDentMin = prod.CaulkingDentMin,
                    CaulkingDentMax = prod.CaulkingDentMax,
                    ShaftLengthMin = prod.ShaftLengthMin,
                    ShaftLengthMax = prod.ShaftLengthMax,
                    SEA_Min = prod.SEA_Min,
                    SEA_Max = prod.SEA_Max,
                    ShaftPullingForce = prod.ShaftPullingForce,
                    BushPullingForce = prod.BushPullingForce,
                    MagnetHeightMin = prod.MagnetHeightMin,
                    MagnetHeightMax = prod.MagnetHeightMax
                };
                // INSERT THE PRODUCT DETAILS 
                //prod.RotorProductID = rotorid;    
                await SqlDataAccess.UpdateInsertQuery("InsertProductinfo", infoparams);           
            }
            return isSuccess;
        }
        public async Task<bool> EditProducts(AddProductDetailsModel prod)
        {
       
            //EDIT THE MAIN MASTERLIST
            string mainQuery = "UPDATE ProdCon_RotorProduct SET  RotorAssy =@RotorAssy,  " +
                     "ProductType =@ProductType, MachinePressureMinMax =@MachinePressureMinMax," +
                     "RecommendedPressureSetting =@RecommendedPressureSetting " +
                     "WHERE RotorProductID =@RotorProductID";
            bool result = await SqlDataAccess.UpdateInsertQuery(mainQuery, prod);
            if (result)
            {
                var infoparams = new
                {
                    RotorProductID = prod.RotorProductID,
                    CaulkingDentMin = prod.CaulkingDentMin,
                    CaulkingDentMax = prod.CaulkingDentMax,
                    ShaftLengthMin = prod.ShaftLengthMin,
                    ShaftLengthMax = prod.ShaftLengthMax,
                    SEA_Min = prod.SEA_Min,
                    SEA_Max = prod.SEA_Max,
                    ShaftPullingForce = prod.ShaftPullingForce,
                    BushPullingForce = prod.BushPullingForce,
                    MagnetHeightMin = prod.MagnetHeightMin,
                    MagnetHeightMax = prod.MagnetHeightMax
                };


                string updateQuery = "UPDATE ProdCon_RotorProductInfo SET  CaulkingDentMin =@CaulkingDentMin,  " +
                     "CaulkingDentMax =@CaulkingDentMax, ShaftLengthMin =@ShaftLengthMin, " +
                     "ShaftLengthMax =@ShaftLengthMax, " +
                     "SEA_Min =@SEA_Min, SEA_Max =@SEA_Max, ShaftPullingForce =@ShaftPullingForce, " +
                     "BushPullingForce =@BushPullingForce, MagnetHeightMin =@MagnetHeightMin, " +
                     "MagnetHeightMax =@MagnetHeightMax " +
                     "WHERE RotorProductID =@RotorProductID";

                await SqlDataAccess.UpdateInsertQuery(updateQuery, prod);
            }
            return result;  
        }

        public  async Task<List<ProductOneModel>> GetOnlyOneProducts(int ID)
        {        
            try
            {
                string strsql = "SELECT " +
                              "r.RotorProductID, r.RotorAssy, r.ProductType, " +
                              "r.MachinePressureMinMax, r.RecommendedPressureSetting, " +
                              "p.CaulkingDentMin, p.CaulkingDentMax, p.ShaftLengthMin, " +
                              "p.ShaftLengthMax, p.SEA_Min, p.SEA_Max, p.ShaftPullingForce, " +
                              "p.BushPullingForce, p.MagnetHeightMin, p.MagnetHeightMax, r.ModelType " +
                           "FROM ProdCon_RotorProduct r " +
                           "INNER JOIN  ProdCon_RotorProductInfo p " +
                           "ON r.RotorProductID = p.RotorProductID " +
                           "WHERE r.RotorProductID = @ID";

                return await SqlDataAccess.GetData<ProductOneModel>(strsql, new { ID = ID});
            }
            catch (FormatException)
            {
                return new List<ProductOneModel>();
            }
            
        }

        public async Task<int> GetShoporderTotalList()
        {
            string strsql = "SELECT COUNT(*) FROM ProdCon_ShopOrder_tbl";
            return await SqlDataAccess.GetCountData(strsql);
        }
    }
}
