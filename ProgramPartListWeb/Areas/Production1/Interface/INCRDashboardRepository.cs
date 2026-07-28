using DocumentFormat.OpenXml.Office2010.ExcelAc;
using ProgramPartListWeb.Areas.Production1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramPartListWeb.Areas.Production1.Interface
{
    public interface INCRDashboardRepository
    {
        Task<List<FourMSummaryModel>> GetFourMSummary();
        Task<List<GroupSummaryModel>> GetGroupSummary();
        Task<List<LineTopNCRModel>> GetBestLines();

        Task<bool> AddAwardsData(AwardDto model);
        Task<bool> EditAwardsData(AwardDto model);
        Task<List<AwardDto>> GetAwardsData();
        Task<bool> DeleteAwardData(int ID);

        Task<string> GetAwardName();

        Task<List<RegistrationFinalModel>> GetRegistrationData(string search, int month);
        Task<bool> AddRegistrationData(RegistrationFinalModel model);
        Task<bool> EditRegistrationData(RegistrationFinalModel model);
        Task<bool> DeleteRegistrationData(int ID);


        Task<List<ProcessGroupsModel>> SetsProcessGroupData(int groups);
    }
}
