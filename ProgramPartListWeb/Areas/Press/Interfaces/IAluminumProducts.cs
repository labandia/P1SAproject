using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Press.Interfaces
{
    public interface IAluminumProducts
    {
        // GET DATA FROM THE LIST
        Task<List<PressMasterlistModel>> GetPressMasterData();
        Task<List<PressIDNote>> GetIDnoteData();
        Task<List<PressTransactHistoryModel>> GetPressHistoryTransactionData(int Act);
        Task<List<IssuanceModel>> GetIssuanceHistory();
        // CRUD OPERATION
        Task<bool> AddNewProducts(AddPressMasterlistModel add);
        Task<bool> InsertSummaryData(object parameters = null);
        Task<bool> UpdateStorageData(int StorageID, int quan);
        Task<bool> UpdateMasterlistData(object parameters, int MastID, int NoteID);
        Task<bool> UpdateIssuance(dynamic parameters = null);
    }
}
