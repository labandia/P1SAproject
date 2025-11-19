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

        Task<List<DieMoldDaily>> GetDailyMoldData(int month, int days,  int year, string process);
        Task<DieMoldDaily> GetDailyLastMoldData(string partnum);

        // ===============================================
        Task<List<DieMoldSummaryProcess>> GetMoldDieSummary();
        Task<List<DieMoldSummaryProcess>> GetMoldDieSummary(string process);
        Task<List<DieMoldTotalPartnum>> GetMoldDieMonthInput(int month, int year, string process);
        Task<List<DieMoldToolingModelDisplay>> GetMoldToolingData();

        Task<List<DieMoldProcess>> GetMoldProcess();
        Task<List<DieProcessDescription>> GetMoldPartDescription();
        // ===============================================
        Task<bool> AddDailyMoldie(DieMoldDailyInput mold);
        Task<bool> UpdateDailyMoldie(DieMoldDailyInput mold);
        Task<bool> UpdateDailyLastCycle(int recordID, int lastcycle);
        Task<bool> DeleteDailyMoldie(int ID);


        Task<bool> AddUpdateMoldie(MoldInputModel mold);
        Task<bool> AddMoldieTooling(DieMoldToolingModel mold);
        Task<bool> UpdateMoldieTooling(DieMoldToolingModel mold);

        Task<bool> AddNewMoldDie(AddDieMoldingDataInput mold);
        Task<bool> UpdateMoldDie(DieMoldingDataInput mold);
        Task<bool> DeleteMoldDie(int Id, string partnum = "");
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
