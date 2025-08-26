using PMACS_V2.Areas.Planning.Model;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.Planning.Interface
{
    public interface IPlanning
    {
        Task<List<DateModel>> GetsDatelist();

        //############ DASHBOARD MONITORING DATA ####################
        Task<DataTable> AdditionalSummary(string dstart, string dend);
        Task<DataTable> LackingResultSummary(string dstart, string dend);
        Task<DataTable> ShopOrderSummary(string dstart, string dend);
        Task<DataTable> GetSalesRequestSummary();
        Task<DataTable> SalesResultSummary();

        Task<List<ProductsModel>> GetSelectedlackDetailsSummary(string strdate, int importID);
        Task<List<ProductsModel>> GetSelectedDetailsSummary(string strdate, int importID);
        Task<List<ShopOrderResultModel>> GetSelectedShopOrderDetailsSummary(string strdate, int importID);
        Task<List<ShopOrderResultModel>> GetSelectedRequestsDetailsSummary(int colint, int rowint, int yearint);
        Task<List<BranchModel>> GetBranchSummary();

        Task<List<MonthlySales>> GetMonthSalesTable();
        //###########################################

        //############ PARTNUMBER SUMMARY DATA ####################
        Task<DataTable> GetDailyPartnumberSummary(string dstart, string dend);
        Task<DataTable> GetSelectedPartnumberSummary(string dstart);
        //###########################################

        //############ DATA MANAGEMENT ####################
        Task<List<Planningmodel>> GetPCDataList();

        Task<List<EndMonthModel>> GetEndMonthlist(string stryear);
        //###########################################

        //############ CHECKS THE DATA ####################
        Task<int> ChecksDailyImport(string DateNowString);

        //############ BUTTON ACTION ######################
        Task<bool> InsertDataExcelFile(List<M1ExcelData> dt, string timecheck);



        Task<bool> AddEndMonthData(object parameters);
        Task<bool> UpdateEndMonthData(PostMontlyEndResult end);
        Task<bool> DeleteEndMonthData(int ID);
        Task<bool> CheckEndMonthExist(string strdate);
    }
}
