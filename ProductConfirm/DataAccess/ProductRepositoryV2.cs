using ProductConfirm.Data;
using ProductConfirm.Models;
using ProgramPartListWeb.Helper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ProductConfirm.DataAccess
{
    public class ProductRepositoryV2 : IProductRepositoryV2
    {
        public ProductRepositoryV2()
        {
        }

        public Task<List<MasterlistModel>> GetMasterlist()
        {
            string strsql = "SELECT RotorProductID, RotorAssy, ProductType, MachinePressureMinMax, " + 
                            "RecommendedPressureSetting, ModelType " +
                            "FROM ProdCon_RotorProduct";
            return SqlDataAccess.GetData<MasterlistModel>(strsql);
        }

        public Task<List<ProductModel>> GetAllProducts()
        {
            string strsql = "ProdMasterlist";
            return SqlDataAccess.GetData<ProductModel>(strsql);
        }

        public Task<List<ExportModel>> GetDataAndExportoExcel()
        {
            string strsql = "ProdMasterlist";
            return SqlDataAccess.GetData<ExportModel>(strsql);
        }

        public Task<List<ProductToolsModel>> GetProductsTools(string shoporder, int ID)
        {
            string strsql = "Measurements";
            var parameters = new { Shoporder = shoporder, ShopOrderID = ID };
            return SqlDataAccess.GetData<ProductToolsModel>(strsql, parameters);
        }

        public Task<List<ShopOrderModel>> GetShoporderlist()
        {
            string strsql = "ShopOrderlist";
            return SqlDataAccess.GetData<ShopOrderModel>(strsql);
        }

        public Task<List<SummaryProductModel>> GetSummaryDataConfirmation()
        {
            string strsql = "SummaryComfirmation";
            return SqlDataAccess.GetData<SummaryProductModel>(strsql);
        }

        // CRUD OPERATION PROCESS
        public async Task<bool> AddProducts(AddProductDetailsModel prod)
        {
            bool issuccess = false;
            // INSERT THE PRODUCT MAIN 
            var mainparams = new
            {
                RotorAssy = prod.RotorAssy,
                ProductType = prod.ProductType,
                MachinePressureMinMax = prod.MachinePressureMinMax,
                RecommendedPressureSetting = prod.RecommendedPressureSetting
            };
            

            issuccess = await SqlDataAccess.UpdateInsertQuery("InsertProduct", mainparams);

            if (issuccess)
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

            return issuccess;
        }
        public async Task<bool> EditProducts(AddProductDetailsModel prod)
        {
            bool result = false;
            //EDIT THE MAIN MASTERLIST
            var mainparams = new
            {
                RotorProductID = prod.RotorProductID,
                RotorAssy = prod.RotorAssy,
                ProductType = prod.ProductType,
                MachinePressureMinMax = prod.MachinePressureMinMax,
                RecommendedPressureSetting = prod.RecommendedPressureSetting
            };


            string mainQuery = "UPDATE ProdCon_RotorProduct SET  RotorAssy =@RotorAssy,  " +
                     "ProductType =@ProductType, MachinePressureMinMax =@MachinePressureMinMax," +
                     "RecommendedPressureSetting =@RecommendedPressureSetting " +
                     "WHERE RotorProductID =@RotorProductID";



            result = await SqlDataAccess.UpdateInsertQuery(mainQuery, mainparams);


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

       
    }
}
