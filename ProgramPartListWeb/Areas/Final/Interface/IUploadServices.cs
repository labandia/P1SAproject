using ProgramPartListWeb.Areas.Final.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Final.Interface
{
    public interface IUploadServices
    {
        //===================================================================
        //=================== GET DATA ======================================
        //===================================================================

        // Gets the total List of Uploaded Data and number of changes
        Task<(int totalrecords, int totalChanges)> GetNumberofUpdatedRecords();
        // Gets the list of uploaded data
        Task<List<UploadProductionRecord>> GetListofUploadedData();
        Task<List<UploadDataModel>> GetListofFailedData();

        //===================================================================
        //=================== UPLOADING PROCESS =============================
        //===================================================================
        // Updates the approval status of the uploaded data 
        Task<bool> CheckApprovalForUploadedData(int recordID, bool check);
        // Insert the uploaded data to the database
        Task<bool> UploadDataToDatabase(ProductionRecord model, string tb);
        Task<bool> TransferDataUploadtoMain();
    }
}
