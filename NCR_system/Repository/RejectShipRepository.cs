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
    internal class RejectShipRepository : IShipRejected
    {
        public async Task<IEnumerable<RejectShipmentModel>> GetRejectedShipData(int proc)
        {
            string strsql = $@"SELECT 
	                          	RecordID,
	                            FORMAT(DateIssued, 'MM/dd/yyyy') as DateIssued,
	                            FORMAT(DateCloseReg, 'MM/dd/yyy') as DateCloseReg,
	                            RegNo,IssueGroup,SectionID,
	                            ModelNo,Quantity,Contents,Status,Process
                            FROM PC_RejectShip
                            WHERE Process =@Process";
            return await SqlDataAccess.GetData<RejectShipmentModel>(strsql, new { Process = proc });
        }

        public Task<bool> InsertNCRData(RejectShipmentModel ncr)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateNCRData(RejectShipmentModel ncr)
        {
            throw new NotImplementedException();
        }
    }
}
