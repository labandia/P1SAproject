﻿using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class MachineRepository : IMachine
    {
        public Task<bool> AddMachine(PostMachineModel model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteMachine(int machID)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditMachine(MachineModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CountMachineModel>> GetCountMachine(int sectionID, string MachineCode)
        {
            string machfilter = MachineCode != "" ? "AND MACH_CODE = " + MachineCode + " " : "";
            string strsql = "SELECT SUM(CASE WHEN IsDelete = 1 THEN 1 ELSE 0 END) as work, " +
                                   "SUM(CASE WHEN IsDelete = 0 THEN 1 ELSE 0 END) as notwork " +
                            "FROM machine WHERE " +
                            "Section_ID = @sectionID " + machfilter + "";

            return await SqlDataAccess.GetData<CountMachineModel>(strsql, new { sectionID = sectionID });
        }

        public async Task<List<EquipmentList>> GetEquipmentData(int sectionID)
        {
            string strsql = "SELECT Machine_code, Equipment, Section_ID FROM Major WHERE Section_ID = " + sectionID + "";
            return await SqlDataAccess.GetData<EquipmentList>(strsql);
        }

        public async Task<List<MachineModel>> GetMachineData(int offset, int limit, int sect, string mach)
        {
            string machfilter = "";
            if (!string.IsNullOrEmpty(mach)){
                machfilter = " AND m.MACH_CODE = '" + mach + "'";
            }

            string strsql = "SELECT m.ID, m.MACH_CODE as machcode, ma.Equipment, m.Machname, m.Model, m.Manufact, " +
                    "m.Serial, m.location, m.Status, m.Filepath, m.Asset, " +
                    "m.Reasons, m.Date_acquired, m.Tongs, m.Section_ID, " +
                    "FORMAT(m.Dateadd, 'MM/dd/yyyy') as Dateadd " +
                    "FROM Machine m INNER JOIN major ma on ma.Machine_code = m.MACH_CODE " +
                    "WHERE m.Section_ID = " + sect + " " + machfilter + " ORDER BY m.ID DESC " +
                    "OFFSET " + offset + " ROWS FETCH NEXT " + limit + " ROWS ONLY ";
            return await SqlDataAccess.GetData<MachineModel>(strsql);
        }
    }
}