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

        public async Task<bool> InsertNCRData(RejectShipmentModel ncr, int proc)
        {
            bool result = false;

            if (proc == 0)
            {
                string strsql = $@"INSERT INTO PC_RejectShip(RegNo, IssueGroup, SectionID, ModelNo, CustomerName, Quantity, Process)
                               VALUES(@RegNo, @IssueGroup, @SectionID, @ModelNo, @CustomerName, @Quantity, @Process)";
                //var parameter = new
                //{
                //    customer.ModelNo,
                //    customer.LotNo,
                //    customer.NGQty,
                //    customer.Details,
                //    customer.SectionID,
                //    customer.RegNo,
                //    customer.CustomerName,
                //    customer.CCtype
                //};
                //result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }
            else
            {
                //string strsql = $@"INSERT INTO PC_CustomerConplaint(ModelNo,LotNo,NGQty,Details,SectionID,CCtype)
                //               VALUES(@ModelNo, @LotNo, @NGQty, @Details, @SectionID, @CCtype)";
                //var parameter = new
                //{
                //    customer.ModelNo,
                //    customer.LotNo,
                //    customer.NGQty,
                //    customer.Details,
                //    customer.SectionID,
                //    customer.CCtype
                //};
                //result = await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
            }

            return result;
        }

        public Task<bool> UpdateNCRData(RejectShipmentModel ncr, int proc)
        {
            throw new NotImplementedException();
        }
    }
}
