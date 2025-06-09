using ProductConfirm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductConfirm.Data
{
    public interface IProductRepository
    {
        // FOR THE MASTERLIST PRODUCT DATA
        Task<DataTable> GetAllProducts();
        Task<DataTable> GetMinandMaxProduct(int rotorid);

        // FOR THE MEASUREMENTS DATA
        Task<DataTable> GetShoporderlist();
        Task<DataTable> GetShoporderDetails(string shoporder, int ID);
        Task<DataTable> GetSummaryDataConfirmation();
        Task<DataTable> GetDataAndExportoExcel();
    }


    public interface IProductRepositoryV2
    {
        // FOR THE MASTERLIST PRODUCT DATA
        Task<List<ProductModel>> GetAllProducts();
        Task<List<MasterlistModel>> GetMasterlist();
        // CRUD OPERATION MASTERLIST
        Task<bool> AddProducts(AddProductDetailsModel prod);
        Task<bool> EditProducts(AddProductDetailsModel prod);

        // FOR THE MEASUREMENTS DATA
        Task<List<ShopOrderModel>> GetShoporderlist();
        Task<List<ProductToolsModel>> GetProductsTools(string shoporder, int ID);
        Task<List<SummaryProductModel>> GetSummaryDataConfirmation();
        Task<List<ExportModel>> GetDataAndExportoExcel();

       
    }


}
