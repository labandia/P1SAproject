using PMACS_V2.Areas.P1SA.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Interface
{
    public interface ICapacity
    {
        //===============  GET MANPOWER DATA ===================
        Task<List<SelectionGroup>> GetGroupCapacity();
        Task<List<PsummaryModel>> GetP1SAsummary();
        

        //========================== P1SA CAPACITY COMMON TO ALL  =====================================
        //Capacity Summary All
        Task<List<CapacitySummaryModel>> GetCapacitySummary(string month, int capid);
        Task<List<string>> GetModelBaseComboxList(int capid);
        Task<List<string>> GetModelBaseDoesntExist(int capid);
        Task<int> GetForecastTotal(string month);
        Task<List<ForecastModel>> GetForecast(string year);
        Task<List<ForecastModel>> GetForecastChart();

        //Molding Capacity
        Task<List<MoldingModel>> GetMoldingModels(string months, int capid);
        Task<bool> AddMoldingModels(AddMoldingModelPost mold);
        Task<bool> EditMoldingModels(MoldingPostmodel mold);

        // Rotor Capacity
        Task<List<RotorModel>> GetRotorModels(string months, int capid);
        Task<bool> AddRotorModels(RotorModel mold);
        Task<bool> EditRotorModels(RotorModel mold);

        // Press Capacity
        Task<List<PressModel>> GetPressModels(string months, int capid);
        Task<bool> AddPressModels(PressModel mold);
        Task<bool> EditPressModels(PressModel mold);

        // Winding Capacity
        Task<List<WindingModel>> GetWindingModels(string months, int capid);
        Task<bool> EditWindingModelBase(WindingModel wind);
        Task<bool> EditWindingProcess(object parameters);
        Task<bool> EditWindingProducts(object parameter);
        Task<bool> EditWindingDetails(WindingModel wind);
        // Circuit Capacity
        Task<List<CircuitModel>> GetCircuitModels(string months, int capid);

        // POST Capacity
        Task<bool> EditP1SAsummary(PsummaryModel cap);

        Task<bool> EditCapacityManpower(object parameters);


        // POST FOR GROUP AND SUMMARY
        Task<bool> UpdateCapacityGroup(CapacityGroupPostModel cap);
        Task<bool> UpdateProcessform(ProcessformPostModel cap);
        Task<bool> UpdateManpower(int totalManpower, string processcode);
        Task<bool> DeleteModels(int capinfo_id, int capgroup);


        Task<bool> UpdateForecast(forecastInput fores, string strsql);
    }
}
