using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace NCR_system.Repository
{
    internal class RejectShipRepository : IShipRejected
    {
        public Task<List<CustomerTotalModel>> GetRejectOpenItem(int section, int Stats)
        {

            string strsection = section != 0 ? $" AND c.SectionID = {section}" : string.Empty;

            string strsql = $@"SELECT  
                                s.DepartmentName,
                                COALESCE(COUNT(c.Status),0) AS totalOpen
                            FROM PC_Section s
                            LEFT JOIN PC_RejectShip c  
                                ON c.SectionID = s.SectionID
                                AND c.IsDeleted = 0 
                                AND c.Status = @Status 
                                AND c.Process = 0
                            {strsection}
                            GROUP BY 
                                s.SectionID,
                                s.DepartmentName
                            ORDER BY
                                s.SectionID ASC;";

            return SqlDataAccess.GetDataAsync<CustomerTotalModel>(strsql, new { Status = Stats });
        }

        public Task<List<CustomerTotalModel>> GetCustomersOpenItem(int Stats, int type = 0, int sec = 0)
        {
            string IsStatus = type == 0 ? "1" : "1, 2, 3";


            string strquery = $@"SELECT 
                                s.DepartmentName,
                                SUM(CASE WHEN c.Status IN ({IsStatus}) 
                                AND c.Process = @Process THEN 1 ELSE 0 END) AS TotalOpen
                            FROM PC_Section s
                            LEFT JOIN PC_RejectShip c
                                ON c.SectionID = s.SectionID WHERE c.IsDeleted = 0 
                            AND c.Status = @Status ";
                          
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Status", Stats);
            // Filter By Process 
            if (sec != 0)
            {
                strquery += @" AND s.SectionID = @SectionID";
                parameters.Add("@SectionID", sec);
            }


            strquery += @" GROUP BY 
                                s.SectionID,
                                s.DepartmentName
                            ORDER BY
                                s.SectionID ASC;";

            parameters.Add("@Process", type);



            return SqlDataAccess.GetDataAsync<CustomerTotalModel>(strquery, parameters);
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

            Debug.WriteLine($@"Section ID : {sectionID} - Status : {stats} - Process : {proc}");

            //string IsStatus = proc == 0 ? "1" : "1, 2, 3";

            //if(stats == 0)
            //{
            //    IsStatus = "0";
            //}

            string strquery = $@"SELECT 
	                          	RecordID,
	                            FORMAT(DateIssued, 'MM/dd/yyyy') as DateIssued,
	                            FORMAT(DateCloseReg, 'MM/dd/yyy') as DateCloseReg,
	                            RegNo,IssueGroup,SectionID,
	                            ModelNo,Quantity,Contents,Status,Process
                            FROM PC_RejectShip
                            WHERE IsDeleted = 0 AND Status =@Status AND Process = @Process ";

            parameters.Add("@Process", proc);
            parameters.Add("@Status", stats);

            // Filter By Section 
            if (sectionID != 0)
            {
                strquery += " AND SectionID = @SectionID";
                parameters.Add("@SectionID", sectionID);
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
            Debug.WriteLine(strquery);

            return await SqlDataAccess.GetDataAsync<RejectShipmentModel>(strquery, parameters);
        }

        public Task<List<CustomerTotalModel>> GetShipmentOpenItem(int stats = 1, int type = 0, int sec = 0)
        {
            throw new NotImplementedException();
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
            return SqlDataAccess.ExecuteAsync(strsql, parameter);
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
            return SqlDataAccess.ExecuteAsync(strsql, parameter);
        }
    }
}
