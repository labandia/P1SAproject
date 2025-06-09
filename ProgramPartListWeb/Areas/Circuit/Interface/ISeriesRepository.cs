using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Interfaces
{
    public interface ISeriesRepository
    {
        // Get Data list
        Task<List<SeriesviewModel>> GetSeriesData();
        Task<List<PrepareviewModel>> GetComponentsList(int seriesID);
        Task<List<SummaryComponentModel>> GetComponentsSummmary(string series);
        Task<List<PartlistModel>> Getpartlist(string series);

        Task<List<SuppliersModel>> GetSuppliersData(string partnum);
        Task<List<ReturnModel>> GetComponentsOutData();
        Task<List<WarehousePreparedModel>> GetWarehousePreparedData(int seriesID);
        Task<List<SummaryHistory>> GetHistoryTransactionData();
        Task<List<SupplierModel>> GetSupplierData();

        Task<bool> CheckComponentsOutData(int ID);
       
        Task<int> GetTotalQuantity(string partnum, int seriesID);

        // INSERT, UPDATE Process
        Task<bool> UpdateSeriesData(object parameters);


        Task<bool> AddEditSuppliers(SupplierModel sup);
        Task<bool> SeriesChangeStatus(int Id, int stats);
        Task<bool> SaveSummaryComponents(object parameters);
        Task<bool> DeleteSummaryComponentList(object parameters);
        Task<bool> UpdatePreparedQuantity(object parameters);
        Task<bool> UpdateSummaryQuantity(object parameters);
        Task<bool> UpdatePartsSummary(int series, string part, int quan);
        Task<bool> UploadComponentsPartlistOut(List<Dictionary<string, string>> rows, int seriesID);
    }
}
