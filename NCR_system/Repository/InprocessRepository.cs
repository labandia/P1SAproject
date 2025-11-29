using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NCR_system.Repository
{
    internal class InprocessRepository : IInprocess
    {
        public Task<CustomerModel> GetCustomerDataByID(int recordID)
        {
            throw new NotImplementedException();
        }

        public Task<List<CustomerTotalModel>> GetCustomersOpenItem(int type = 0)
        {
            string strsql = $@"SELECT 
                                    s.DepartmentName,
                                    COUNT(c.Status) AS totalOpen
                                FROM PC_Section s
                                LEFT JOIN PC_Inprocess c
                                    ON c.SectionID = s.SectionID 
                                    AND c.Status = 1 
                                GROUP BY 
                                    s.SectionID, 
                                    s.DepartmentName
                                ORDER BY 
                                s.SectionID ASC;";
            return SqlDataAccess.GetData<CustomerTotalModel>(strsql, null);
        }



        public async Task<IEnumerable<InprocessModel>> GetInprocessData(int section)
        {
            string query = $@"SELECT RecordID,DateEncounter
                                  ,TitleEmail,Shift
                                  ,Line,Model
                                  ,ShopOrder,Defect
                                  ,NGQty,ProcEncounter
                                  ,cause,Invest
                                  ,Status,P1saStatus
                                  ,Remarks,SectionDep, SectionID
                              FROM PC_Inprocess
                              WHERE SectionID =@SectionID";
            return await SqlDataAccess.GetData<InprocessModel>(query, new { SectionID = section });
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
