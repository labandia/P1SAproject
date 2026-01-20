using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Circuit.Repository
{
    public class MetalMaskTransactionServices : IMetalMast_Transaction
    {
        public Task<List<MetalMaskTransaction>> GetMetalMaskSMTransaction(string partnum, int SMTLine)
        {
            throw new NotImplementedException();
        }

        public Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(
            string partnum, 
            int Stats, 
            int SMTLine)
        {
            string strsql = $@"SELECT t.RecordID, t.DateInput
                                      ,t.Shift, t.SMTLine
                                      ,t.Partnumber, t.AREA
                                      ,t.SMT_start, t.SMT_end
                                      ,t.TotalTime, t.TotalPrintBoard
                                      ,t.SMT_Operator, t.CleanDate
                                      ,t.Pattern, t.Frame
                                      ,t.RevisionNo,t.ReadOne
                                      ,t.ReadTwo,t.ReadThree
                                      ,t.ReadFour,t.Result
                                      ,t.Remarks,t.PIC, t.Status
                                  FROM MetalMask_Transaction t 
                                  INNER JOIN MetalMask_Masterlist m ON t.Partnumber = m.Partnumber
                                  WHERE t.IsDelete = 0 ";

            return SqlDataAccess.GetData<MetalMaskTransaction>(strsql);
        }

        public Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(string search, int Stats, int ModelType, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }
        public Task<MetalMaskTransaction> GetMetalMaskTransacDetails()
        {
            throw new NotImplementedException();
        }


        public Task<bool> AddMetalMastTransaction(MetalMaskTransaction metal)
        {
            string strsql = $@"INSERT INTO MetalMask_Transaction(Partnumber, AREA, SMTLine)
                            VALUES(@Partnumber, @AREA, @SMTLine)";
            return SqlDataAccess.UpdateInsertQuery(strsql, metal);
        }

        public Task<bool> DeleteMetalMastTransaction(int ID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMetalMastTransaction(MetalMaskTransaction metal)
        {
            throw new NotImplementedException();
        }

        
    }
}