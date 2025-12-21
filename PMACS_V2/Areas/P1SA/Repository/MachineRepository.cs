using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class MachineRepository : IMachine
    {
        public Task<bool> AddMachine(PostMachineModel model)
        {
            string strsql = $@"INSERT INTO machine(MACH_CODE, Machname, Model, Serial, Manufact, Date_acquired, Shifts, 
                                                   Location, Status, Asset, IsDelete, Section_ID, Filepath, Dateadd, Reasons, Tongs)
                               VALUES(@MACH_CODE, @Equipment, @Model, @Serial, @Manufact, @Date_acquired, @Shifts, 
                                                   @Location, @Status, @Asset, @IsDelete, @Section_ID, @Filepath, @Dateadd, @Reasons, @Tongs)";
            return SqlDataAccess.UpdateInsertQuery(strsql, model);    
        }

        public Task<bool> DeleteMachine(int machID)
        {
            string strsql = $@"UPDATE machine SET IsDelete = 0 WHERE ID =@ID";
            return SqlDataAccess.UpdateInsertQuery(strsql, new { ID = machID});
        }

        public Task<bool> EditMachine(EditMachineModel model)
        {
            string strsql = $@"UPDATE machine SET Machname =@Machname, Date_acquired =@Date_acquired, Model =@Model, 
                               Location =@Location, Serial =@Serial, Shifts =@Shifts, Manufact =@Manufact, Asset =@Asset, 
                               Tongs =@Tongs, Status =@Status, Reasons =@Reasons, Filepath =@Filepath
                              WHERE ID =@ID";



            return SqlDataAccess.UpdateInsertQuery(strsql, model);
        }

        public Task<List<CountMachineModel>> GetCountMachine(int sectionID, string MachineCode)
        {
            string machfilter = MachineCode != "" ? $@"AND MACH_CODE = '" + MachineCode + "' " : "";
            string strsql = $@"SELECT SUM(CASE WHEN IsDelete = 1 THEN 1 ELSE 0 END) as work, 
                                     SUM(CASE WHEN IsDelete = 0 THEN 1 ELSE 0 END) as notwork 
                            FROM machine WHERE 
                            Section_ID = @sectionID  {machfilter}";

            return SqlDataAccess.GetData<CountMachineModel>(strsql, new { sectionID = sectionID });
        }

        public Task<List<EquipmentList>> GetEquipmentData(int sectionID)
        {
            string strsql = "SELECT Machine_code, Equipment, Section_ID FROM Major WHERE Section_ID = " + sectionID + "";
            return SqlDataAccess.GetData<EquipmentList>(strsql, null);
        }

        public Task<List<MachineModel>> GetMachineData(int offset, int limit, int sect, string mach)
        {
            string machfilter = (!string.IsNullOrEmpty(mach)) ? $@" AND m.MACH_CODE = '{mach}'" : "";
            
            string strsql = $@"SELECT m.ID, m.MACH_CODE as machcode, ma.Equipment, m.Machname, m.Model, m.Manufact, 
                    m.Serial, m.location, m.Status, m.Filepath, m.Asset, m.Shifts,
                    m.Reasons, m.Date_acquired, m.Tongs, m.Section_ID, 
                    FORMAT(m.Dateadd, 'MM/dd/yyyy') as Dateadd 
                    FROM Machine m INNER JOIN major ma on ma.Machine_code = m.MACH_CODE 
                    WHERE m.Section_ID = {sect} {machfilter} ORDER BY m.ID DESC 
                    OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY ";
            return SqlDataAccess.GetData<MachineModel>(strsql, null);
        }

        public  Task<List<MachineModel>> GetMachineDataByID(int ID)
        {
            string strsql = $@"SELECT m.ID, m.MACH_CODE as machcode, ma.Equipment, m.Machname, m.Model, m.Manufact, 
                    m.Serial, m.location, m.Status, m.Filepath, m.Asset, m.Shifts,
                    m.Reasons, m.Date_acquired, m.Tongs, m.Section_ID, 
                    FORMAT(m.Dateadd, 'MM/dd/yyyy') as Dateadd 
                    FROM Machine m INNER JOIN major ma on ma.Machine_code = m.MACH_CODE
                    WHERE m.ID =@ID";
            return  SqlDataAccess.GetData<MachineModel>(strsql, new { ID = ID });
        }


        public async Task<byte[]> GetMachineImage(int machineID)
        {
            string sql = "SELECT Filepath FROM machine WHERE ID = @ID";
            return await SqlDataAccess.GetByteAsync(sql, new { ID = machineID });
        }


    }
}