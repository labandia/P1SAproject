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
    internal class NCR_Repository : INCR
    {
        public async Task<IEnumerable<NCRModels>> GetNCRData(int type)
        {
            string strsql = $@"SELECT RecordID,DateIssued ,RegNo,Category
                              ,IssueGroup,SectionID,ModelNo
                              ,Quantity,Contents
                              ,Status,CircularStatus
                              ,DateCloseReg,FilePath
                              ,Remarkstat,Process
                              ,TargetDate,Reviewer,DateRegist
                          FROM PC_NCR";
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
