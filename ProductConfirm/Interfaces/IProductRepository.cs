using ProductConfirm.Models;
using System.Collections.Generic;
using System.Data;
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
        Task<List<ProductOneModel>> GetOnlyOneProducts(int ID);     
        Task<List<MasterlistModel>> GetMasterlist();
        Task<List<CaulkingDent>> GetCaulkingDentData(int ID);

        // CRUD OPERATION MASTERLIST
        Task<bool> AddProducts(AddProductDetailsModel prod);
        Task<bool> EditProducts(AddProductDetailsModel prod);

        // FOR THE MEASUREMENTS DATA
        Task<List<ShopOrderModel>> GetShoporderlist(int CurrentPageIndex, int PageSize);
        Task<int> GetShoporderTotalList();
        Task<List<ProductToolsModel>> GetProductsTools(string shoporder, int ID);
        Task<List<SummaryProductModel>> GetSummaryDataConfirmation();
        Task<List<ExportModel>> GetDataAndExportoExcel();


        // SHOP ORDER DATA LIST PER PROCESS
        Task<bool> UpdateShopOrderStatus(int stats, int Item, int ID);
        Task<bool> UpdateAndAddSuppliers(string shop, string supp, string lot, int shopID);

        Task<bool> AddAndUpdateCaulkingDentData(CaulkingDentInput cd);
    }


}
