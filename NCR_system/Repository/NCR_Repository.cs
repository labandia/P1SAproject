using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class NCR_Repository : INCR
    {
        public async Task<List<NCRModels>> GetNCRData(string search,
            string Category,
            int stats,
            int section,
            int type)
        {
            string strquery = $@"SELECT RecordID
                                  ,DateIssued
                                  ,RegNo
                                  ,Category
                                  ,IssueGroup
                                  ,SectionID
                                  ,ModelNo
                                  ,Quantity
                                  ,Contents
                                  ,Status
                                  ,CircularStatus
                                  ,DateCloseReg
                                  ,FilePath
                                  ,Remarkstat
                                  ,Process
                                  ,TargetDate
                                  ,Reviewer
                                  ,DateRegist
                                  ,UploadImage
                              FROM PC_NCR
                              WHERE IsDelete = 0 AND Process =@Type ";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Type", type);

            if(Category != "")
            {
                strquery += @" Category =@Category";
                parameters.Add("@Category", Category);
            }

            if(stats != 0)
            {
                strquery += @" Status =@Status";
                parameters.Add("@Status", stats);
            }

            if (section != 0)
            {
                strquery += @" SectionID =@SectionID";
                parameters.Add("@SectionID", section);
            }

            return await SqlDataAccess.GetDataAsync<NCRModels>(strquery, parameters);
        }

        public async Task<List<NCRDatamodel>> GetSummaryNCR(int type)
        {
            string strsql = $@"SELECT
                                s.DepartmentName AS Section,

                                SUM(CASE WHEN n.Status = 1 AND n.Category = 'PATROL' THEN 1 ELSE 0 END) AS Patrol,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 'INPROCESS' THEN 1 ELSE 0 END) AS Inprocess,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 'NEXT PROCESS' THEN 1 ELSE 0 END) AS NextProcess,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 'CALIBRATION' THEN 1 ELSE 0 END) AS Calibration,
                                SUM(CASE WHEN n.Status = 1 AND n.Category = 'DELAY SHIPMENT' THEN 1 ELSE 0 END) AS Shipment_Delay,

                                SUM(CASE WHEN n.Status = 1 THEN 1 ELSE 0 END) AS TotalOpen,

                                SUM(CASE WHEN n.Status = 5 THEN 1 ELSE 0 END) AS Circular,
                                SUM(CASE WHEN n.Status = 2 THEN 1 ELSE 0 END) AS Report,
                                SUM(CASE WHEN n.Status = 3 THEN 1 ELSE 0 END) AS WithReport,
                                SUM(CASE WHEN n.Status = 4 THEN 1 ELSE 0 END) AS WithReportReview

                            FROM PC_Section s
                            LEFT JOIN PC_NCR n
                                ON n.SectionID = s.SectionID
                                AND n.IsDelete = 0
	                            AND n.Process = @Type
                            GROUP BY s.DepartmentName, s.SectionID
                            ORDER BY s.SectionID;";
            return await SqlDataAccess.GetDataAsync<NCRDatamodel>(strsql, new { Type = type });
        }

        public Task<bool> InsertNCRData(NCRModels ncr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNCRData(NCRModels ncr)
        {
            throw new NotImplementedException();
        }
    }
}
