using PMACS_V2.Areas.P1SA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Interface
{
    public interface IDieMold
    {
        // ===============================================
        Task<List<DieMoldTotalPartnum>> GetMoldTotalPartNoList(int month, int year);
        Task<List<DieMoldSetNotal>> GetSummaryMoldData();


        // ===============================================
        Task<List<DieMoldSummaryProcess>> GetMoldDieSummary();
        Task<List<DieMoldTotalPartnum>> GetMoldDieMonthInput(int month, int year);
        // ===============================================

        Task<bool> AddUpdateMoldie(MoldInputModel mold);


        // PRESS
        // ===============================================
        Task<List<PressDieRegistry>> GetPressRegistryList();
        Task<List<PressDieMontoring>> GetPressMonitoring();
    }
}
