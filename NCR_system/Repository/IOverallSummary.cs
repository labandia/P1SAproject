using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    public class IOverallSummary : ISummaryNCR
    {
        public Task<List<SummaryNCRModel>> GetCustomerSummary(DateTime datecreated)
        {
            return SqlDataAccess.GetDataAsync<SummaryNCRModel>(@"SELECT 
	                    s.SectionID,
                        s.DepartmentName,
                        COUNT(CASE 
                            WHEN c.CCtype = 1 
                                 AND c.Status = 0 
                                 AND c.IsDelete = 0 
                            THEN 1 END) AS SDC,
    
                        COUNT(CASE 
                            WHEN c.CCtype = 2 
                                 AND c.Status = 0 
                                 AND c.IsDelete = 0 
                            THEN 1 END) AS ExternalCC

                    FROM PC_Section s
                    LEFT JOIN PC_CustomerConplaint c
                        ON c.SectionID = s.SectionID

                    GROUP BY s.SectionID, s.DepartmentName
                    ORDER BY s.SectionID ASC;");
        }

        public Task<List<SummaryInprocessModel>> GetInprocessSummary()
        {
            return SqlDataAccess.GetDataAsync<SummaryInprocessModel>(@"SELECT 
	                s.SectionID,
                    s.DepartmentName,
                    COUNT(CASE 
                        WHEN c.Status = 0 
                        THEN 1 END) AS OpenCase
                FROM PC_Section s
                LEFT JOIN PC_Inprocess c
                    ON c.SectionID = s.SectionID
	                AND c.IsDelete = 0
                GROUP BY s.SectionID, s.DepartmentName
                ORDER BY s.SectionID;");
        }

        public Task<List<SummaryInprocessModel>> GetRejectedSummary()
        {
            return SqlDataAccess.GetDataAsync<SummaryInprocessModel>($@"  SELECT 
	                    s.SectionID,
                        s.DepartmentName,
                        COUNT(CASE 
                            WHEN
								 c.Process = 0
								 AND c.Status = 0 
                                 AND c.IsDeleted = 0 
                            THEN 1 END) AS OpenCase
                    FROM PC_Section s
                    LEFT JOIN PC_RejectShip c
                        ON c.SectionID = s.SectionID

                    GROUP BY s.SectionID, s.DepartmentName
                    ORDER BY s.SectionID ASC;");
        }

        public Task<List<SummaryInprocessModel>> GetShipmentSummary()
        {
            return SqlDataAccess.GetDataAsync<SummaryInprocessModel>($@"  
                        SELECT 
	                    s.SectionID,
                        s.DepartmentName,
                        COUNT(CASE 
                            WHEN
								 c.Process = 1
								  AND c.Status IN (1, 2, 3)
                                 AND c.IsDeleted = 0 
                            THEN 1 END) AS OpenCase
                    FROM PC_Section s
                    LEFT JOIN PC_RejectShip c
                        ON c.SectionID = s.SectionID

                    GROUP BY s.SectionID, s.DepartmentName
                    ORDER BY s.SectionID ASC;");
        }

        public Task<List<OverallNCR>> GetNCRRegistrationSummary()
        {
            return SqlDataAccess.GetDataAsync<OverallNCR>(@"SELECT
                    s.DepartmentName AS SectionName,
                    SUM(CASE WHEN n.Status = 1 THEN 1 ELSE 0 END) AS OpenTotals
                FROM PC_Section s
                LEFT JOIN PC_NCR n
                    ON n.SectionID = s.SectionID
                    AND n.IsDelete = 0
                GROUP BY s.DepartmentName, s.SectionID
                ORDER BY s.SectionID;");
        }
    }
}
