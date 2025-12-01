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
        public async Task<IEnumerable<NCRModels>> GetNCRData(int type)
        {
            string strsql = $@"SELECT 
	                            RecordID,
	                            FORMAT(DateIssued, 'MM/dd/yy') as DateIssued,
	                            RegNo,Category
	                            ,IssueGroup,SectionID,ModelNo
	                            ,Quantity,Contents
	                            ,Status,CircularStatus
	                            ,FORMAT(DateCloseReg, 'MM/dd/yy') as DateCloseReg,
	                            FilePath
	                            ,Remarkstat,Process
	                            ,TargetDate,Reviewer,DateRegist
                            FROM PC_NCR WHERE Process =@Type";
            return await SqlDataAccess.GetData<NCRModels>(strsql, new { Type = type });
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
