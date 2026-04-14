using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class NCR_Repository : INCR
    {
        public async Task<List<MainNCRModel>> GetNCRData(string search,
            int Category,
            int stats,
            int section,
            int type)
        {
            string strquery = $@"SELECT RecordID
                                  ,Category
                                  ,RegNo
                                  ,DateIssued
                                  ,IssueGroup
                                  ,SectionID
                                  ,ModelNo
                                  ,Quantity
                                  ,Contents
                                  ,Status
                                  ,CircularStatus
                                  ,DateCloseReg
                                  ,FilePath
                                  ,Process
                                  ,TargetDate
                                  ,Reviewer
                                  ,DateRegist
                                  ,UploadImage  
                              FROM PC_NCR
                              WHERE IsDelete = 0 AND Process =@Type ";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Type", type);


            strquery += @" AND Status =@Status";
            parameters.Add("@Status", stats);

            if (Category != 0)
            {
                strquery += @" AND Category =@Category";
                parameters.Add("@Category", Category);
            }

            

            if (section != 0)
            {
                strquery += @" AND SectionID =@SectionID";
                parameters.Add("@SectionID", section);
            }


            return await SqlDataAccess.GetDataAsync<MainNCRModel>(strquery, parameters);
        }

        public Task<List<NCRDatamodel>> GetSummaryNCR(int type)
        {
            string strsql = $@"SELECT
                                s.DepartmentName AS Section,

                                SUM(CASE WHEN n.Status = 1 AND n.Category = 1 THEN 1 ELSE 0 END) AS Patrol,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 2 THEN 1 ELSE 0 END) AS Inprocess,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 3 THEN 1 ELSE 0 END) AS NextProcess,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 4 THEN 1 ELSE 0 END) AS Calibration,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 5 THEN 1 ELSE 0 END) AS Shipment_Delay,

                                SUM(CASE WHEN n.Status = 1 THEN 1 ELSE 0 END) AS TotalOpen,

                                SUM(CASE WHEN n.Status = 5 THEN 1 ELSE 0 END) AS Circular,
                                SUM(CASE WHEN n.Status = 2 THEN 1 ELSE 0 END) AS Report,
                                SUM(CASE WHEN n.Status = 3 THEN 1 ELSE 0 END) AS WithReport,
                                SUM(CASE WHEN n.Status = 4 THEN 1 ELSE 0 END) AS WithReportReview

                            FROM PC_Section s
                            LEFT JOIN PC_NCR n
                                ON n.SectionID = s.SectionID
                                AND n.IsDelete = 0
	                            AND n.Process = 0
                            GROUP BY s.DepartmentName, s.SectionID
                            ORDER BY s.SectionID;";
            return  SqlDataAccess.GetDataAsync<NCRDatamodel>(strsql, new { Type = type });
        }

        public Task<bool> InsertNCRData(NCRModels ncr)
        {
            return SqlDataAccess.ExecuteAsync($@"INSERT INTO PC_NCR(Category, RegNo, DateIssued, SectionID, ModelNo, Quantity, Contents, Status, 
                                               CircularStatus,  FilePath, Process, DateRegist, UploadImage)
                                               VALUES(@Category, @RegNo, @DateIssued, @SectionID, @ModelNo, @Quantity, @Contents, @Status, 
                                               @CircularStatus,  @FilePath, @Process, @DateRegist, @UploadImage)", ncr);
        }

        public Task<bool> UpdateNCRData(NCRModels ncr)
        {
            throw new NotImplementedException();
        }
    }
}
