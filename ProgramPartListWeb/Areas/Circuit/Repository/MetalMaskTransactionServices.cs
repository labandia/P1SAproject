using Aspose.Cells.Drawing;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using ProgramPartListWeb.Areas.Circuit.Interface;
using ProgramPartListWeb.Areas.Circuit.Models;
using ProgramPartListWeb.Helper;
using ProgramPartListWeb.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                                      ,m.ModelType
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
            string strsql = $@"UPDATE MetalMask_Transaction SET CleanDate = GETDATE(),
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
                ReadOne = metal.ReadOne,
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
	                (SELECT COUNT(Status) FROM MetalMask_Transaction WHERE Status = 0 AND IsDelete = 0) as SMTCount, 
	                (SELECT COUNT(Status) FROM MetalMask_Transaction WHERE Status = 1 AND IsDelete = 0) as TensionCount
                FROM MetalMask_Transaction
                GROUP BY Status");
        }

        public async Task<PagedResult<MetalMaskTransaction>> GetTransactINComplete(
            string partnum, 
            int com,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string condition = com != 0
            ? @" AND t.ReadOne   BETWEEN 30 AND 50
                 AND t.ReadTwo   BETWEEN 30 AND 50
                 AND t.ReadThree BETWEEN 30 AND 50
                 AND t.ReadFour  BETWEEN 30 AND 50"
            : @" AND (
                    t.ReadOne   NOT BETWEEN 30 AND 50
                 OR t.ReadTwo   NOT BETWEEN 30 AND 50
                 OR t.ReadThree NOT BETWEEN 30 AND 50
                 OR t.ReadFour  NOT BETWEEN 30 AND 50
                )";

            string countQuery = $@" SELECT COUNT(*) 
                                    FROM MetalMask_Transaction t
                                    WHERE IsDelete = 0 
                                      AND Status = 2 {condition}";

            string strquery = $@" SELECT
                            t.RecordID
                            ,t.DateInput
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
                    WHERE t.IsDelete = 0 AND t.Status = 2 {condition} ";


            // Filter By Partnumber
            if (!string.IsNullOrEmpty(partnum))
            {
                strquery += " AND t.Partnumber = @Partnumber";
                parameters.Add("@Partnumber", partnum);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY t.RecordID DESC OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            var items = await SqlDataAccess.GetData<MetalMaskTransaction>(strquery, parameters);


            // Now get the total count
            int TotalRecords = await SqlDataAccess.GetCountDataSync(countQuery, parameters);

            return new PagedResult<MetalMaskTransaction>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = TotalRecords
            };
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