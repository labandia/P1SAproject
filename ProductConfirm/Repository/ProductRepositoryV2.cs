using ProductConfirm.Data;
using ProductConfirm.Models;
using ProductConfirm.Modules;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        public Task<List<CaulkingDent>> GetCaulkingDentData(int ID)
        {
            string strsql = $@"SELECT c.Shoporder, sp.CD_supply, sp.CD_lot,
                            c.CD_first, c.CD_second, c.CD_third, c.CD_fourth, c.CD_fifth, 
	                        c.CD_six, c.CD_seven, c.CD_eight, c.CD_nine, c.CD_ten, 
	                        c.CD_eleven, c.CD_twelve, c.CD_thirteen, CD_14, CD_15, CD_16,
	                        c.CD_17, c.CD_18, c.CD_19, c.CD_20, c.CD_21, c.CD_22, c.CD_23, c.CD_24, 
	                        c.CD_25, c.CD_26, c.CD_27, c.CD_28, c.CD_29, c.CD_30, c.CD_31, c.CD_32,
	                        c.CD_33, c.CD_34, c.CD_35, c.CD_36, c.CD_37, c.CD_38, c.CD_39, c.CD_40
                          FROM ProdCon_CaulkingDentData c
                          INNER JOIN ProdCon_ShopOrder_supplier sp ON sp.ShopOrderID = c.ShopOrderID
                          AND c.ShopOrderID = @ShopOrderID";
            return SqlDataAccess.GetData<CaulkingDent>(strsql, new { ShopOrderID = ID });
        }
        public Task<List<ProductToolsModel>> GetProductsTools(string shoporder, int ID)
        {
            string strsql = "Measurements";
            var parameters = new { Shoporder = shoporder, ShopOrderID = ID };
            return SqlDataAccess.GetData<ProductToolsModel>(strsql, parameters);
        }

        public Task<List<ShopOrderModel>> GetShoporderlist(int CurrentPageIndex, int pageSize)
        {
            return  SqlDataAccess.GetData<ShopOrderModel>("ShopOrderlist", 
                new { PageNumber = CurrentPageIndex, PageSize = pageSize });
        }  
        public Task<List<SummaryProductModel>>GetSummaryDataConfirmation()
        {
            return SqlDataAccess.GetData<SummaryProductModel>("SummaryComfirmation");
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

        public Task<bool> UpdateShopOrderStatus(int stats, int Item, int ID)
        {
            string updatesmeasure = $@"UPDATE ProdCon_ShopOrderData_tbl SET Status =@Status 
                                    WHERE Item_ID =@Item_ID AND ShopOrderID =@ShopOrderID";
            return SqlDataAccess.UpdateInsertQuery(updatesmeasure, new { Status = stats, Item_ID = Item, ShopOrderID = ID});
        }

        public async Task<bool> UpdateAndAddSuppliers(string shop, string supp, string lot, int shopID)
        {
            string strsupply = $@"SELECT COUNT(p.Shoporder)  FROM ProdCon_ShopOrder_supplier p
                               WHERE p.ShopOrderID = @ShopOrderID";

            if(await SqlDataAccess.Checkdata(strsupply))
            {
                string updatesql = $@"UPDATE ProdCon_ShopOrder_supplier SET
                                     CD_supply =@CD_supply, CD_lot =@CD_lot
                                     WHERE Shoporder =@Shoporder AND ShopOrderID =@ShopOrderID";
                return await SqlDataAccess.UpdateInsertQuery(updatesql, new { Shoporder = shop, CD_lot = lot, CD_supply = supp, ShopOrderID = shopID });
            }
            else 
            {
                // INSERT THE THE SHOPORDER SUPPLIER
                string measurequery = "INSERT INTO ProdCon_ShopOrder_supplier(Shoporder, CD_supply, CD_lot, ShopOrderID) " +
                 "VALUES (@Shoporder, @CD_supply, @CD_lot, @ShopOrderID)";

                return await SqlDataAccess.UpdateInsertQuery(measurequery, new { Shoporder = shop, CD_lot = lot, CD_supply = supp, ShopOrderID = shopID });
            }

        }

        public async Task<bool> AddAndUpdateCaulkingDentData(CaulkingDentInput cd)
        {
            string stractualData = $@"SELECT COUNT(p.Shoporder) FROM ProdCon_CaulkingDentData p 
                             WHERE p.ShopOrderID = @ShopOrderID";
            if (await SqlDataAccess.Checkdata(stractualData))
            {
                string updatesqldata = $@"UPDATE ProdCon_ShopOrder_ActualData SET 
                                   CD_first =@CD_first, CD_second =@CD_second, CD_third =@CD_third, 
                                   CD_fourth =@CD_fourth, CD_fifth =@CD_fifth, CD_six =@CD_six, 
                                   CD_seven =@CD_seven, CD_eight =@CD_eight, CD_nine =@CD_nine, 
                                   CD_ten =@CD_ten, CD_eleven =@CD_eleven, CD_twelve =@CD_twelve, 
                                   CD_thirteen =@CD_thirteen, CD_14 =@CD_14, CD_15 =@CD_15, CD_16 =@CD_16,
                                   CD_17 =@CD_17, CD_18 =@CD_18, CD_19 =@CD_19, CD_20 =@CD_20,
                                   CD_21 =@CD_21, CD_22 =@CD_22, CD_23 =@CD_23, CD_24 =@CD_24,
                                   CD_25 =@CD_25, CD_26 =@CD_26, CD_27 =@CD_27, CD_28 =@CD_28,
                                   CD_29 =@CD_29, CD_30 =@CD_30, CD_31 =@CD_31, CD_32 =@CD_32,
                                   CD_33 =@CD_33, CD_34 =@CD_34, CD_35 =@CD_35, CD_36 =@CD_36,
                                   CD_37 =@CD_37, CD_38 =@CD_38, CD_39 =@CD_39, CD_40 =@CD_40
                                   WHERE Shoporder = @Shoporder AND ShopOrderID =@ShopOrderID";
                return await SqlDataAccess.UpdateInsertQuery(updatesqldata, cd);
            }
            else
            {
                // INSERT THE THE SHOPORDER ACTUAL DATA
                string dataquery = $@"INSERT INTO ProdCon_ShopOrder_ActualData(Shoporder, ShopOrderID, CD_first, CD_second, CD_third, 
                                     CD_fourth, CD_fifth, CD_six, CD_seven, CD_eight, CD_nine, CD_ten, CD_eleven, CD_twelve, CD_thirteen
                                     CD_14, CD_15, CD_16, CD_17, CD_18, CD_19, CD_20, CD_21, CD_22, CD_23, CD_24, CD_25, CD_26, CD_27
                                     CD_28, CD_29, CD_30, CD_31, CD_32, CD_33, CD_34, CD_35, CD_36, CD_37, CD_38, CD_39, CD_40) 
                    VALUES (@Shoporder, @ShopOrderID, @CD_first, @CD_second, @CD_third, 
                                     @CD_fourth, @CD_fifth, @CD_six, @CD_seven, @CD_eight, @CD_nine, @CD_ten, @CD_eleven, @CD_twelve, @CD_thirteen
                                     @CD_14, @CD_15, @CD_16, @CD_17, @CD_18, @CD_19, @CD_20, @CD_21, @CD_22, @CD_23, @CD_24, @CD_25, @CD_26, @CD_27
                                     @CD_28, @CD_29, @CD_30, @CD_31, @CD_32, @CD_33, @CD_34, @CD_35, @CD_36, @CD_37, @CD_38, @CD_39, @CD_40)";
                return await SqlDataAccess.UpdateInsertQuery(dataquery, cd);
            }
        }
    }
}
