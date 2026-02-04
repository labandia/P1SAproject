using Aspose.Cells.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
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
        public Task<bool> AddMetalMastTransaction(MetalMaskTransaction metal)
        {
            string strsql = $@"INSERT INTO MetalMask_Transaction(Partnumber, Shift, AREA, SMTLine, Status)
                            VALUES(@Partnumber, @Shift, @AREA, @SMTLine, @Status)";
            return SqlDataAccess.UpdateInsertQuery(strsql, metal);
        }

        public Task<bool> DeleteMetalMastTransaction(int ID)
        {
            string strsql = $@"UPDATE MetalMask_Transaction SET IsDelete = 1
                               WHERE RecordID =@RecordID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { RecordID  = ID });
        }

        public Task<bool> EditMetalMastTransaction(MetalMaskTransaction metal)
        {
            throw new NotImplementedException();
        }

       

        public Task<MetalMaskTransaction> GetMetalMaskTransacDetails(int RecordID)
        {
            string strquery = $@"SELECT t.RecordID, t.DateInput
                                      ,t.Shift
                                      ,t.SMTLine
                                      ,t.Partnumber, t.AREA, m.Blocks
                                      ,t.SMT_start, t.SMT_end
                                      ,t.TotalTime, t.TotalPrintBoard
                                      ,t.SMT_Operator, t.CleanDate
                                      ,t.Pattern, t.Frame
                                      ,t.ReadOne
                                      ,t.ReadTwo,t.ReadThree
                                      ,t.ReadFour,t.Result
                                      ,t.Remarks,t.PIC, t.Status
                                  FROM MetalMask_Transaction t 
                                  INNER JOIN MetalMask_Masterlist m ON t.Partnumber = m.Partnumber
                                  WHERE t.IsDelete = 0 AND t.RecordID =@RecordID";

            return SqlDataAccess.GetObjectOnly<MetalMaskTransaction>(strquery, new { RecordID = RecordID });
        }

        public Task<List<MetalMaskTransaction>> GetMetalMaskTransaction(
            string search,
            string partnum,
            int SMTLine,
            int Stats,
            int ModelType,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string strquery = $@"SELECT t.RecordID, t.DateInput
                                      ,t.Shift
                                      ,t.SMTLine
                                      ,t.Partnumber, t.AREA, m.Blocks
                                      ,t.SMT_start, t.SMT_end
                                      ,t.TotalTime, t.TotalPrintBoard
                                      ,t.SMT_Operator, t.CleanDate
                                      ,t.Pattern, t.Frame
                                      ,t.ReadOne
                                      ,t.ReadTwo,t.ReadThree
                                      ,t.ReadFour,t.Result
                                      ,t.Remarks,t.PIC, t.Status
                                  FROM MetalMask_Transaction t 
                                  INNER JOIN MetalMask_Masterlist m ON t.Partnumber = m.Partnumber
                                  WHERE t.IsDelete = 0 ";

            // Filter By Partnumber
            if (!string.IsNullOrEmpty(partnum))
            {
                strquery += "AND t.Partnumber = @Partnumber";
                parameters.Add("@Partnumber", partnum);
            }

            // Filter By SMT Line
            if (SMTLine != 0)
            {
                strquery += "AND t.SMTLine = @SMTLine";
                parameters.Add("@SMTLine", SMTLine);
            }

            // Filter By Model Type
            if (ModelType != 0)
            {
                strquery += "AND m.ModelType = @ModelType";
                parameters.Add("@ModelType", ModelType);
            }

            // Filter By Status 
            strquery += $@" AND t.Status = @Status";
            parameters.Add("@Status", Stats);

            // Search Partnumber
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND (
                                @Search IS NULL
                                OR t.Partnumber LIKE '%' + @Search + '%'
                              )";
                parameters.Add("@Search", search);
            }

            strquery += $@" ORDER BY t.RecordID DESC";

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            return SqlDataAccess.GetData<MetalMaskTransaction>(strquery, parameters);
        }

        public Task<bool> StartOperation(int ID)
        {
            TimeSpan startTime = DateTime.Now.TimeOfDay;

            string strsql = $@"UPDATE MetalMask_Transaction SET SMT_start =@SMT_start 
                               WHERE RecordID =@RecordID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                SMT_start = startTime,
                RecordID = ID
            });
        }

        public Task<bool> EndOperation(int ID)
        {
            TimeSpan startTime = DateTime.Now.TimeOfDay;

            string strsql = $@"UPDATE MetalMask_Transaction SET SMT_end =@SMT_end 
                               WHERE RecordID =@RecordID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                SMT_end = startTime,
                RecordID = ID
            });
        }

        public Task<bool> SMTsubmitTransaction(
            MetalMaskTransaction metal)
        {
            string strsql = $@"UPDATE MetalMask_Transaction SET 
                            TotalPrintBoard =@TotalPrintBoard, Status = Status + 1,
                            SMT_Operator =@SMT_Operator
                               WHERE RecordID =@RecordID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                TotalPrintBoard = metal.TotalPrintBoard,
                SMT_Operator = metal.SMT_Operator,
                RecordID = metal.RecordID
            });
        }

        public Task<bool> TensionsubmitTransaction(MetalMaskTransaction metal)
        {
            string strsql = $@"UPDATE MetalMask_Transaction SET CleanDate =@CleanDate,
                            Pattern =@Pattern, Frame =@Frame, ReadOne =@ReadOne, ReadTwo =@ReadTwo,
                            ReadThree =@ReadThree, ReadFour =@ReadFour, Result =@Result,    
                            Remarks =@Remarks, PIC =@PIC, Status = 2
                            WHERE RecordID =@RecordID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                CleanDate = DateTime.Now,
                Pattern = metal.Pattern,
                Frame = metal.Frame,
                ReadOne = metal.RecordID,
                ReadTwo = metal.ReadTwo,
                ReadThree = metal.ReadThree,
                ReadFour = metal.ReadFour,
                Result = metal.Result,
                Remarks = metal.Remarks,
                PIC = metal.PIC,
                RecordID = metal.RecordID
            });
        }

        public Task<MetalMasKCountTransact> GetTheTotalCount()
        {
            return SqlDataAccess.GetObjectOnly<MetalMasKCountTransact>($@"SELECT 
	                TOP 1
	                (SELECT COUNT(Status) FROM MetalMask_Transaction WHERE Status = 0) as SMTCount, 
	                (SELECT COUNT(Status) FROM MetalMask_Transaction WHERE Status = 1) as TensionCount
                FROM MetalMask_Transaction
                GROUP BY Status");
        }

        public Task<List<MetalMaskTransaction>> GetTransactINComplete(string partnum)
        {
            return SqlDataAccess.GetData<MetalMaskTransaction>($@"SELECT 
                           RecordID,
                           DateInput,
                           Shift,
                           SMTLine,
                           Partnumber,
                           AREA,
                           SMT_start,
                           SMT_end,
                           TotalTime,
                           TotalPrintBoard,
                           SMT_Operator,
                           CleanDate,
                           Pattern,
                           Frame,
                           ReadOne,
                           ReadTwo,
                           ReadThree,
                           ReadFour,
                           Result,
                           Remarks,
                           PIC,
                           Status,
                           IsDelete
                    FROM MetalMask_Transaction
                    WHERE 
                          Status = 2
                      AND Partnumber = @Partnumber
                      AND (
                             ReadOne   NOT BETWEEN 30 AND 50
                          OR ReadTwo   NOT BETWEEN 30 AND 50
                          OR ReadThree NOT BETWEEN 30 AND 50
                          OR ReadFour  NOT BETWEEN 30 AND 50
                          );", new { Partnumber  = partnum });
        }

        public Task<bool> UpdateMetalMaskIncomplete(MetalMaskTransaction metal)
        {
            string strsql = $@"UPDATE MetalMask_Transaction SET  ReadOne =@ReadOne, ReadTwo =@ReadTwo,
                            ReadThree =@ReadThree, ReadFour =@ReadFour
                            WHERE RecordID =@RecordID";

            return SqlDataAccess.UpdateInsertQuery(strsql, new
            {
                ReadOne = metal.ReadOne,
                ReadTwo = metal.ReadTwo,
                ReadThree = metal.ReadThree,
                ReadFour = metal.ReadFour,
                RecordID = metal.RecordID
            });
        }
    }
}