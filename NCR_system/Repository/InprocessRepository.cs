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
    internal class InprocessRepository : IInprocess
    {
        public async Task<IEnumerable<InprocessModel>> GetInprocessData(int section)
        {
            string query = $@"SELECT RecordID,DateEncounter
                                  ,TitleEmail,Shift
                                  ,Line,Model
                                  ,ShopOrder,Defect
                                  ,NGQty,ProcEncounter
                                  ,cause,Invest
                                  ,Status,P1saStatus
                                  ,Remarks,SectionDep
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
