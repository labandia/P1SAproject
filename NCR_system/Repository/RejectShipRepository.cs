﻿using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
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

        public  Task<bool> InsertShipRejectData(RejectShipmentModel ncr, int Process)
        {

            string strsql = $@"INSERT INTO PC_RejectShip(RegNo, DateIssued,  IssueGroup, SectionID, ModelNo, Quantity, Contents, DateCloseReg, Status,  Process)
                               VALUES(@RegNo, @DateIssued,  @IssueGroup, @SectionID, @ModelNo, @Quantity, @Contents, @DateCloseReg, @Status, @Process)";
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
