using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class RejectShipRepository : IShipRejected
    {
        public Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0, int sec = 0)
        {
            string IsStatus = type == 0 ? "1" : "1, 2, 3";


            string strquery = $@"SELECT 
                                s.DepartmentName,
                                SUM(CASE WHEN c.Status IN ({IsStatus}) AND c.Process = @Process THEN 1 ELSE 0 END) AS TotalOpen,
                                SUM(CASE WHEN c.Status = 0 AND c.Process = @Process THEN 1 ELSE 0 END) AS TotalClosed
                            FROM PC_Section s
                            LEFT JOIN PC_RejectShip c
                                ON c.SectionID = s.SectionID ";
                          

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            // Filter By Process 
            if (sec != 0)
            {
                strquery += @"WHERE c.IsDeleted = 0 AND s.SectionID = @SectionID";
                parameters.Add("@SectionID", sec);
            }


            strquery += @" GROUP BY 
                                s.SectionID,
                                s.DepartmentName
                            ORDER BY
                                s.SectionID ASC;";

            parameters.Add("@Process", type);

            return SqlDataAccess.GetData<CustomerTotalModel>(strquery, parameters);
        }

        public async Task<List<RejectShipmentModel>> GetRejectedShipData(
            int sectionID,
            int stats,
            int proc,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string strquery = $@"SELECT 
	                          	RecordID,
	                            FORMAT(DateIssued, 'MM/dd/yyyy') as DateIssued,
	                            FORMAT(DateCloseReg, 'MM/dd/yyy') as DateCloseReg,
	                            RegNo,IssueGroup,SectionID,
	                            ModelNo,Quantity,Contents,Status,Process
                            FROM PC_RejectShip
                            WHERE IsDeleted = 0";

            // Filter By Process 
            if (proc != 0)
            {
                strquery += " AND Process = @Process";
                parameters.Add("@Process", proc);
            }


            // Filter By Section 
            if (sectionID != 0)
            {
                strquery += " AND SectionID = @SectionID";
                parameters.Add("@SectionID", sectionID);
            }

            // Filter By Status Type 
            if (stats != 0)
            {
                strquery += " AND Status = @Status";
                parameters.Add("@Status", stats);
            }

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" ORDER BY RecordID ASC
                            OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }

            return await SqlDataAccess.GetData<RejectShipmentModel>(strquery, parameters);
        }

        public  Task<bool> InsertShipRejectData(RejectShipmentModel ncr, int Process)
        {

            string strsql = $@"INSERT INTO PC_RejectShip(RegNo, DateIssued,  IssueGroup, SectionID, ModelNo, Quantity, Contents, DateCloseReg, Status,  Process, UploadImage)
                               VALUES(@RegNo, @DateIssued,  @IssueGroup, @SectionID, @ModelNo, @Quantity, @Contents, @DateCloseReg, @Status, @Process, @UploadImage)";
            var parameter = new
            {
                ncr.RegNo,
                DateIssued = Convert.ToDateTime(ncr.DateIssued),
                ncr.IssueGroup,
                ncr.SectionID,
                ncr.ModelNo,
                ncr.Quantity,
                ncr.Contents,
                DateCloseReg = Convert.ToDateTime(ncr.DateCloseReg),
                ncr.Status,
                Process,
                ncr.UploadImage
            };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }

        public Task<bool> UpdateShipRejectData(RejectShipmentModel ncr)
        {
            string strsql = $@"UPDATE PC_RejectShip SET RegNo =@RegNo, DateIssued =@DateIssued , IssueGroup =@IssueGroup,
                             SectionID =@SectionID, Status =@Status, ModelNo =@ModelNo, Quantity =@Quantity, Contents =@Contents, DateCloseReg =@DateCloseReg
                             WHERE RecordID = @RecordID";

            var parameter = new
            {
                ncr.RecordID, 
                ncr.RegNo,
                DateIssued = Convert.ToDateTime(ncr.DateIssued),
                ncr.IssueGroup,
                ncr.SectionID,
                ncr.Status,
                ncr.ModelNo,
                ncr.Quantity,
                ncr.Contents,
                DateCloseReg = Convert.ToDateTime(ncr.DateCloseReg),
            };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
    }
}
