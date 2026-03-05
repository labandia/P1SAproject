using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class InprocessRepository : IInprocess
    {
    
        public Task<List<CustomerTotalModel>> GetCustomersOpenItem(int Stats = 1, int sec = 0)
        {
            string strquery = $@"SELECT 
                                    s.DepartmentName,
                                    COUNT(c.Status) AS totalOpen
                                FROM PC_Section s
                                LEFT JOIN PC_Inprocess c
                                    ON c.SectionID = s.SectionID 
                                    AND c.Status = @Status AND IsDelete = 0 ";

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@Status", Stats);

            // Filter By Process 
            if (sec != 0)
            {
                strquery += @" WHERE c.SectionID = @SectionID";
                parameters.Add("@SectionID", sec);
            }

            strquery += @" GROUP BY 
                                s.SectionID,
                                s.DepartmentName
                            ORDER BY
                                s.SectionID ASC;";
            //Debug.WriteLine(strquery);

            return SqlDataAccess.GetDataAsync<CustomerTotalModel>(strquery, parameters);
        }

        public async Task<List<InprocessModel>> GetInprocessData(
            string search,
            int departmentID,
            int Stats,
            int pageNumber,
            int pageSize)
        {
            int offset = (pageNumber - 1) * pageSize;

            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string strquery = $@"SELECT RecordID, DateEncounter
                                  ,TitleEmail,Shift
                                  ,Line,Model
                                  ,ShopOrder,Defect
                                  ,NGQty,ProcEncounter
                                  ,cause,Invest
                                  ,Status,P1saStatus
                                  ,Remarks,SectionDep, SectionID
                              FROM PC_Inprocess
                              WHERE [IsDelete] = 0 ";

            // Filter By SMT Line
            if (departmentID != 0)
            {
                strquery += " AND SectionID = @SectionID";
                parameters.Add("@SectionID", departmentID);
            }

            // Filter By SMT Line
            if (Stats != 0)
            {
                strquery += " AND Status = @Status";
                parameters.Add("@Status", Stats);
            }

            // Search Text
            if (!string.IsNullOrEmpty(search))
            {
                strquery += $@" AND ShopOrder LIKE '%' + @Search + '%'";
                parameters.Add("@Search", search);
            }

            strquery += $@" ORDER BY RecordID DESC";

            // If the Get Data has a Pagination function
            if (pageSize != 0)
            {
                strquery += $@" OFFSET @Offset ROWS
                            FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add("@Offset", offset);
                parameters.Add("@PageSize", pageSize);
            }


            return await SqlDataAccess.GetDataAsync<InprocessModel>(strquery, parameters);
        }

        public Task<bool> InsertInprocessData(InprocessModel inprocess)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateInprocessData(InprocessModel inprocess)
        {
            throw new NotImplementedException();
        }
    }
}
