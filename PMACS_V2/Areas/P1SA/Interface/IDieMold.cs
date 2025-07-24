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
        // MOLDING
        // ===============================================
        Task<List<DieMoldTotalPartnum>> GetMoldTotalPartNoList(int month, int year, string process);
        Task<List<DieMoldSetNotal>> GetSummaryMoldData();


        // ===============================================
        Task<List<DieMoldSummaryProcess>> GetMoldDieSummary(string process);
        Task<List<DieMoldTotalPartnum>> GetMoldDieMonthInput(int month, int year, string process);
        Task<List<DieMoldToolingModelDisplay>> GetMoldToolingData();
        // ===============================================

        Task<bool> AddUpdateMoldie(MoldInputModel mold);
        Task<bool> AddMoldieTooling(DieMoldToolingModel mold);

        // PRESS
        // ===============================================
        Task<List<PressDieSummary>> GetPressSummary();
        Task<List<PressDieRegistry>> GetPressRegistryList();
        Task<List<PressDieMontoring>> GetPressMonitoring();
        Task<List<PressMainMonitor>> GetPressMainMonitoring();
        Task<List<PressDieControlModel>> GetPressControl();

        Task<bool> AddUpdatePressMonitoring(PressInputModel press);
        Task<bool> AddPressMonitorData(PressMonitorInput press);
        Task<bool> UpdateEndofLifeMonitorData(string ToolNo);

        Task<bool> AddPressRegistry(PressDieRegistry press);
        Task<bool> AddPressDieControl(PressDieControlData press);
        Task<bool> EditPressRegistry(PressDieRegistry press);
    }
}
