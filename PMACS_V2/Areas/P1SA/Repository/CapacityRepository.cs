using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Office.Interop.Excel;
using PMACS_V2.Areas.P1SA.Interface;
using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class CapacityRepository : ICapacity
    {
        // ===========================================================
        // ==================== P1SA Summary =========================
        // ===========================================================
        public Task<List<PsummaryModel>> GetP1SAsummary() => SqlDataAccess.GetData<PsummaryModel>("P1SAsummary", null);
        public Task<List<SelectionGroup>> GetGroupCapacity() => SqlDataAccess.GetData<SelectionGroup>("Selectiongroup");
        public  Task<List<CapacitySummaryModel>> GetCapacitySummary(string month, int capid)
        {
            string strquery;

            if (capid == 1 || capid == 2 || capid == 3)
            {
                string CycleTime = (capid == 3) ? "c.CycleTime" : "c.CycleTime / c.Head_count";

                strquery = "WITH AvgCycle AS (SELECT ProcessCode, AVG(cycle) as TotalCycle, " +
					                                "ProcessName, OperationTime, AvailMachine, Months, Days,Cap_Per_Machine " +
                                             "FROM(SELECT  c.CycleTime / c.Head_count  as cycle, " +
                                                  "cap.ProcessCode, cap.ProcessName, cap.OperationTime, " +
                                                  "cap.AvailMachine, cap.Months, cap.Days, cap.Cap_Per_Machine, cap.CycleTime " +
                                             "FROM PMACS_Capacity_Winding  c " +
                                             "INNER JOIN PMACS_ProdProcess cap ON cap.ProcessCode = c.ProcessCode " +
                                             "WHERE c.Capgroup_ID = 1  AND c.IsDelete = 1  " +
                                             "GROUP BY c.Product_type, cap.ProcessCode, c.CycleTime, c.Head_count, " +
                                             "cap.ProcessName, cap.Months, cap.OperationTime, cap.AvailMachine, cap.Days," +
                                             "cap.Cap_Per_Machine, cap.CycleTime) as sub " +
                                             "GROUP BY sub.ProcessCode,  sub.ProcessName, sub.Months, " +
				                             "sub.OperationTime, sub.AvailMachine, sub.Days, sub.Cap_Per_Machine), " + 
                            "SumForedata AS(SELECT  ProcessCode, SUM(foreData) as foredata, SUM(manhour) as Manhour, SUM(manpower) as RequiredMan " +
                                                "FROM(SELECT cap.ProcessName, c.Model_name, cap.ProcessCode, cap.Days, cap.OperationTime, " +
                                                    "cap.AvailMachine, cap.Months, f.April as foreData, " +
                                                "COALESCE(f.April *  c.CycleTime  / 60 / 60 / 24, 0.0)  as manhour, " +
                                                "COALESCE(f.April *  c.CycleTime  / 60 / 60 / 24 / c.Operation_time, 0.0) as manpower " +
                                            "FROM PMACS_Capacity_Winding c LEFT JOIN PMACS_Forecast f on f.Model_name = c.Model_name " +
                                            "INNER JOIN PMACS_ProdProcess cap on cap.ProcessCode = c.ProcessCode " +
                                            "WHERE c.Capgroup_ID = 1 AND c.IsDelete = 1) as sub GROUP BY sub.ProcessCode, sub.AvailMachine, sub.ProcessName) " +
                            "SELECT AvgCycle.ProcessCode, AvgCycle.TotalCycle as CycleTime, " +
                                "AvgCycle.ProcessName,  AvgCycle.OperationTime, AvgCycle.Days, AvgCycle.Months, " + 
                                "SumForedata.foredata as Forecast, AvgCycle.AvailMachine,  " +
                                "(AvgCycle.Days * 3600) / AvgCycle.TotalCycle * AvgCycle.OperationTime as Cap_Per_Machine,  " +
                                "(AvgCycle.Days * 3600) / AvgCycle.TotalCycle * AvgCycle.OperationTime * AvgCycle.AvailMachine as Capday,  " +
                                "(AvgCycle.Days * 3600) / AvgCycle.TotalCycle * AvgCycle.OperationTime * AvgCycle.AvailMachine * AvgCycle.Months   as Capmonth,  " +
                                "SumForedata.Manhour AS Totalhours, " +
                                "SumForedata.RequiredMan as RequiredMan " +
                            "FROM AvgCycle " +
                            "LEFT JOIN SumForedata ON AvgCycle.ProcessCode = SumForedata.ProcessCode " +
                            "ORDER BY AvgCycle.ProcessCode";

            }
            else if (capid == 4 || capid == 5 || capid == 6)
            {
                strquery = "SELECT ProcessName, CycleTime,  Days, Months, OperationTime, " +
                                "SUM(forecast) as Forecast, AvailMachine, " +
                                "(Days * 3600) / CycleTime * OperationTime as Cap_Per_Machine, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine as Capday, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine * Months as Capmonth, " +
                                "ROUND(SUM(manhour), 1) as Totalhours, " +
                                "ROUND(SUM(Manpower), 0) as RequiredMan, " +
                                "ProcessCode " +
                            "FROM(SELECT  cap.ProcessName, m.ProcessCode, cap.CycleTime, " +
                                "COALESCE(f." + month + ", 0) as forecast, cap.Days, cap.Months, cap.OperationTime, " +
                                "cap.AvailMachine, " +
                                "COALESCE((f." + month + " * m.CycleTime / 60 / 60), 0.0)  as manhour, " +
                                "COALESCE((f." + month + " * m.CycleTime / 60 / 60 / 24 / m.Operation_time * 1.9), 0.0) as Manpower " +
                            "FROM PMACS_Capacity_Rotor m " +
                            "LEFT JOIN PMACS_Forecast f ON m.Model_name = f.Model_name " +
                            "INNER JOIN PMACS_ProdProcess cap ON m.ProcessCode = cap.ProcessCode " +
                            "WHERE m.Capgroup_ID = " + capid + " AND m.IsDelete = 1) as Sub " +
                            "GROUP BY Sub.ProcessCode, Sub.ProcessName, Sub.AvailMachine, Sub.Days, Sub.Months, " +
                            "Sub.OperationTime, Sub.CycleTime " +
                            "ORDER BY sub.ProcessCode";
            }
            else if (capid == 7 || capid == 8 || capid == 9)
            {
                strquery = "SELECT " +
                                "ProcessName, CycleTime, Months, Days, OperationTime, " +
                                "SUM(forecast) as Forecast, AvailMachine, " +
                                "(Days * 3600) / CycleTime * OperationTime as Cap_Per_Machine, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine  as Capday, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine * Months  as Capmonth, " +
                                "ROUND(SUM(manhour), 1) as Totalhours, " +
                                "ROUND(SUM(Manpower *  2), 0) as RequiredMan, " +
                                "ProcessCode " +
                            "FROM(SELECT  cap.ProcessName, cap.ProcessCode, cap.CycleTime, " +
                                "COALESCE(f." + month + ", 0) as forecast, cap.Days, cap.Months, cap.OperationTime, " +
                                "cap.AvailMachine, " +
                                "COALESCE((f." + month + " * (m.CycleTime / m.Actual_cav) / 60 /60), 0.0)  as manhour, " +
                                "COALESCE((f." + month + " * (m.CycleTime / m.Actual_cav) / 60 /60 / 24 / m.Operation_time), 0.0) as Manpower " +
                            "FROM PMACS_Capacity_Molding m " +
                            "LEFT JOIN PMACS_Forecast f ON m.Model_name = f.Model_name " +
                            "INNER JOIN PMACS_ProdProcess cap ON m.ProcessCode = cap.ProcessCode " +
                            "WHERE m.Capgroup_ID = " + capid + " AND m.IsDelete = 1)  as Subquery " +
                            "GROUP BY Subquery.Days, Subquery.Months,  Subquery.Days, Subquery.OperationTime, " +
                            "Subquery.ProcessName, Subquery.ProcessCode, Subquery.AvailMachine, Subquery.CycleTime";
            }
            else if (capid == 10 || capid == 11 || capid == 12)
            {
                string CycleTime;

                switch (capid)
                {
                    case 10:
                        CycleTime = "(COALESCE(ROUND((60 / NULLIF(p.SPM, 0) / NULLIF(p.Row, 0) * p.Lam), 1), 0.00))";
                        break;
                    case 12:
                        CycleTime = "COALESCE(CONVERT(DECIMAL(10, 4), (p.TotalCycle * 1.0 / p.Bucket)) + (p.Cycle_cnc + p.Apearance_inspect), 0.0)";
                        break;
                    default:
                        CycleTime = "p.CycleTime";
                        break;
                }
     
                strquery = "SELECT " +
                                "ProcessName, CycleTime, Months, Days, OperationTime, " +
                                "SUM(forecast) as Forecast, AvailMachine, " +
                                "(Days * 3600) / CycleTime * OperationTime as Cap_Per_Machine, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine  as Capday, " +
                                "(Days * 3600) / CycleTime * OperationTime * AvailMachine * Months  as Capmonth, " +
                                "ROUND(SUM(manhour), 1) as Totalhours, " +
                                "ROUND(SUM(Manpower *  2), 0) as RequiredMan, " +
                                 "ProcessCode " +
                            "FROM(SELECT  cap.ProcessName, cap.ProcessCode, cap.CycleTime, " +
                                "cap.OperationTime, cap.AvailMachine, " +
                                "COALESCE(f." + month + ", 0) as forecast, cap.Days, cap.Months, " +
                                "COALESCE((f." + month + " * " + CycleTime + ") / 60 /60, 0.0)  as manhour, " +
                                "COALESCE((f." + month + " * " + CycleTime + ") / 60 /60 / 24 / NULLIF(p.Operation_time, 0), 0.0) as Manpower, " +
                                "cap.Cap_Per_Machine " +
                                "FROM PMACS_Capacity_Press p " +
                            "LEFT JOIN PMACS_Forecast f ON p.Model_name = f.Model_name " +
                            "INNER JOIN PMACS_ProdProcess cap ON p.ProcessCode = cap.ProcessCode " +
                            "WHERE p.Capgroup_ID = " + capid + " AND p.IsDelete = 1) " +
                            "as Subquery  " +
                            "GROUP BY Subquery.Days, Subquery.AvailMachine, Subquery.Months, " +
                            "Subquery.ProcessName, Subquery.ProcessCode, Subquery.OperationTime, Subquery.Cap_Per_Machine, " +
                            "Subquery.CycleTime";

            }
            else
            {
                strquery = "SELECT " +
	                            "ProcessName, CycleTime, Months, Days, OperationTime, " +
	                            "SUM(forecast) as Forecast, AvailMachine, " +
	                            "(Days * 3600) / CycleTime * OperationTime as Cap_Per_Machine, " +
	                            "(Days * 3600) / CycleTime * OperationTime * AvailMachine as Capday, " +
	                            "(Days * 3600) / CycleTime * OperationTime * AvailMachine * Months as Capmonth, " +
	                            "ROUND(SUM(manhour), 1) as Totalhours, " +
	                            "ROUND(SUM(Manpower *  2), 0) as RequiredMan, " +
	                            "ProcessCode " +
                            "FROM(SELECT m.Model_name,  cap.ProcessName, cap.ProcessCode, cap.CycleTime, " +
                            "COALESCE(f." + month + ", 0) as forecast, cap.AvailMachine, cap.Days, cap.Months, cap.OperationTime, " +
                            "COALESCE((f." + month + " * (m.CycleTime) / 60 /60), 0.0)  as manhour, " +
                            "COALESCE((f." + month + " * (m.CycleTime) / 60 /60 / 24 / m.Operation_time), 0.0) as Manpower, " +
                            "cap.Cap_Per_Machine " +
                            "FROM  PMACS_Capacity_Circuit m  " +
                            "LEFT JOIN PMACS_Forecast f ON m.Model_name = f.Model_name " +
                            "INNER JOIN PMACS_ProdProcess cap ON m.ProcessCode = cap.ProcessCode " +
                            "WHERE m.Capgroup_ID = " + capid + ")  as Subquery  " +
                            "GROUP BY Subquery.Days, Subquery.AvailMachine, Subquery.Months, Subquery.CycleTime, " +
                            "Subquery.ProcessName, Subquery.ProcessCode,  " +
                            "Subquery.OperationTime, Subquery.Cap_Per_Machine";
            }

            return SqlDataAccess.GetData<CapacitySummaryModel>(strquery, null);
        }
        public Task<int> GetForecastTotal(string month)
        {
            return SqlDataAccess.GetCountData("SELECT SUM(" + month + ") as total FROM PMACS_Forecast");
        }
        public Task<List<string>> GetModelBaseComboxList(int capid)
        {
            string strTables = "";

            if (capid == 1 || capid == 2 || capid == 3)
            {
                strTables = "PMACS_Capacity_Winding";
            }
            if (capid == 4 || capid == 5 || capid == 6)
            {
                strTables = "PMACS_Capacity_Rotor";
            }
            if (capid == 7 || capid == 8 || capid == 9)
            {
                strTables = "PMACS_Capacity_Molding";
            }
            if (capid == 10 || capid == 11 || capid == 12)
            {
                strTables = "PMACS_Capacity_Press";
            }
            if (capid == 13 || capid == 14 || capid == 15)
            {
                strTables = "PMACS_Capacity_Circuit";
            }

            string strsqlquery = "SELECT c.Model_name " +
                                 "FROM " + strTables + " c " +
                                 "LEFT JOIN PMACS_Forecast f " +
                                 "ON f.Model_name = c.Model_name " +
                                 "WHERE c.Capgroup_ID = " + capid + " AND c.IsDelete = 1";
            return  SqlDataAccess.GetlistStrings(strsqlquery);
        }
        public async Task<List<string>> GetModelBaseDoesntExist(int capid)
        {
            string strTables = "";

            if (capid == 1 || capid == 2 || capid == 3)
            {
                strTables = "PMACS_Capacity_Winding";
            }
            if (capid == 4 || capid == 5 || capid == 6)
            {
                strTables = "PMACS_Capacity_Rotor";
            }
            if (capid == 7 || capid == 8 || capid == 9)
            {
                strTables = "PMACS_Capacity_Molding";
            }
            if (capid == 10 || capid == 11 || capid == 12)
            {
                strTables = "PMACS_Capacity_Press";
            }
            if (capid == 13 || capid == 14 || capid == 15)
            {
                strTables = "PMACS_Capacity_Circuit";
            }

            string strsqlquery = "SELECT f.Model_name " +
                                "FROM PMACS_Forecast f " +
                                "WHERE NOT EXISTS(SELECT 1 FROM " + strTables + " c " +
                                "WHERE(f.Model_name = c.Model_name AND c.Capgroup_ID = 7) AND c.IsDelete = 1)";
            return await SqlDataAccess.GetlistStrings(strsqlquery);
        }
        public Task<List<ForecastModel>> GetForecast(string year) => SqlDataAccess.GetData<ForecastModel>("ForecastData", null);
        public Task<List<ForecastModel>> GetForecastChart() => SqlDataAccess.GetData<ForecastModel>("ForecastChart", null);
        // ===========================================================
        // ==================== Capacity per Section =================
        // ===========================================================
        // COMMON FUNCTION TO ALL
        public Task<bool> EditCapacityManpower(object parameters)
        {
            throw new System.NotImplementedException();
        }


       
        // Molding Section
        public Task<List<MoldingModel>> GetMoldingModels(string months, int capid)
        {
            string strsql = "SELECT DISTINCT m.Capinfo_ID, (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) as Cyclepcs, " +
                            "m.DieQty, m.Capgroup_ID, " +
                            "m.Actual_cav, cp.Days, m.CycleTime, m.Operation_time, " +
                            "f.Model_name, m.Partnum, cp.Months, " +
                            "COALESCE(f." + months + ", 0) as foredata, m.ProcessCode, " +
                            "COALESCE((f." + months + " * (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) / 60 / 60), 0.0)  as manhour, " +
                            "COALESCE((f." + months + " * (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) / 60 / 60 / 24 / m.Operation_time), 0.0) as Require, " +  
                            "COALESCE((f." + months + " * (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) / 60 / 60 / 24 / m.Operation_time), 0.0) as Manpower, " + 
                            "(cp.Days * 3600 / (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) *  cp.OperationTime) as Capday, " +
                            "(cp.Days * 3600 / (NULLIF(m.CycleTime, 0) / NULLIF(m.Actual_cav, 0)) *  cp.OperationTime)  * cp.Months as Capmonth " +
                            "FROM PMACS_Forecast f " +
                            "LEFT JOIN PMACS_Capacity_Molding m ON  m.Model_name = f.Model_name AND m.Capgroup_ID = " + capid  + 
                            "LEFT JOIN PMACS_ProdProcess cp on cp.ProcessCode = m.ProcessCode " +
                            "WHERE IsDelete = 1 " +
                            "ORDER BY m.Capgroup_ID DESC";

           return SqlDataAccess.GetData<MoldingModel>(strsql, new { Capgroup_ID = capid });
        }
        public async Task<bool> AddMoldingModels(AddMoldingModelPost mold)
        {
            int IsDelete = 1;

            string Check_Existing_data = $@"SELECT m.Model_name FROM PMACS_Capacity_Molding m WHERE 
                                        (m.model_name = N'@model_name' AND m.IsDelete = 0) 
                                        AND  m.Capgroup_ID = @Capgroup_ID";
            var Checkparams = new { model_name = mold.Model_name, Capgroup_ID = mold.Capgroup_ID };
            bool Checkresult = await SqlDataAccess.Checkdata(Check_Existing_data, Checkparams);

            //If the Add model is already Inserted. Updates the information only
            if (Checkresult)
            {
                string UpdateMoldingQuery = $@"UPDATE PMACS_Capacity_Molding SET Partnum =@Partnum, CycleTime  =@CycleTime, 
                              DieQty =@DieQty, Actual_cav =@Actual_cav, IsDelete =@IsDelete, Operation_time =@Operation_time 
                              WHERE (model_name = @model_name AND IsDelete = 0) AND  Capgroup_ID = @Capgroup_ID";
                var Updateparams = new { Partnum = mold.Partnum, CycleTime = mold.CycleTime, Model_name = mold.Model_name, DieQty = mold.DieQty,
                                         Actual_cav = mold.Actual_cav, Operation_time = mold.Operation_time, IsDelete = IsDelete, Capgroup_ID = mold.Capgroup_ID};

                //if the query is not Update successfully
                return await SqlDataAccess.UpdateInsertQuery(UpdateMoldingQuery, Updateparams); ;
            } 
            else
            {
                string InsertMoldingQuery = $@"INSERT INTO PMACS_Capacity_Molding(Partnum, CycleTime, Model_name, 
                                              DieQty, Actual_cav, Operation_time, Capgroup_ID, ProcessCode) 
                                              VALUES(@Partnum, @CycleTime, @Model_name, 
                                              @DieQty, @Actual_cav, @Operation_time, @Capgroup_ID, @ProcessCode)";
                var Insertparams = new
                {
                    Partnum = mold.Partnum,
                    CycleTime = mold.CycleTime,
                    Model_name = mold.Model_name,
                    DieQty = mold.DieQty,
                    Actual_cav = mold.Actual_cav,
                    Operation_time = mold.Operation_time,
                    Capgroup_ID = mold.Capgroup_ID,
                    ProcessCode = mold.ProcessCode
                };

                return await SqlDataAccess.UpdateInsertQuery(InsertMoldingQuery, Insertparams); 
            }
        }
        public  Task<bool> EditMoldingModels(MoldingPostmodel mold) =>  SqlDataAccess.UpdateInsertQuery("UpdateMoldingdetails", mold);
        // Press Section
        public Task<List<PressModel>> GetPressModels(string months, int capid)
        {
            string CycleTime = "";

            switch (capid)
            {
                case 10:
                    CycleTime = "(COALESCE(ROUND((60 / NULLIF(p.SPM, 0) / NULLIF(p.Row, 0) * p.Lam), 1), 0.00))";
                    break;
                case 12:
                    CycleTime = "COALESCE(CONVERT(DECIMAL(10, 4), (p.TotalCycle * 1.0 / p.Bucket)) + (p.Cycle_cnc + p.Apearance_inspect), 0.0)";
                    break;
                default:
                    CycleTime = "p.CycleTime";
                    break;
            }

            string strsql = "SELECT p.Capinfo_ID, " +
                                "COALESCE(" + CycleTime + ", 0.0) as Cyclepcs, " +
	                            "p.Product_type, COALESCE(" + CycleTime + ", 0.0) as CycleTime, " +
	                            "p.Model_name, COALESCE(p.DieQty, 0.0) as DieQty,  p.Operation_time, " +
	                            "p.Partnum, p.Cycle_cnc, p.Cycle_drill, p.Apearance_inspect, " +
	                            "p.Pre_wash, p.Wash_cycle, p.Bucket, p.Laser, p.TotalCycle, p.CycleTap, " +
	                            "COALESCE(p.SPM, 0.0) as SPM, COALESCE(p.Row, 0.0) as Row, " +
	                            "COALESCE(p.Lam, 0.0) as Lam, " +
	                            "COALESCE(f." + months + ", 0) as foredata, p.ProcessCode, " +
	                            "COALESCE(NULLIF(f." + months + " * " + CycleTime + " / 60 / 60, 0.00), 0.00)  as manhour, " +
	                            "COALESCE(NULLIF(f." + months + " * " + CycleTime + " / 60 / 60 / 24 / NULLIF(p.Operation_time, 0), 0.00), 0.00)  as Require, " +
	                            "COALESCE(NULLIF(f." + months + " * " + CycleTime + " / 60 / 60 / 24 / NULLIF(p.Operation_time, 0), 0.00), 0.00) as Manpower, " +
	                            "cap.AvailMachine, cap.Days, cap.Months, cap.OperationTime, " +
	                            "(cap.Days * 3600 / CASE WHEN " + CycleTime + " = 0 THEN 1 ELSE " + CycleTime + " END * p.Operation_time)  as Capday, " +
	                            "(cap.Days * 3600 / CASE WHEN " + CycleTime + " = 0 THEN 1 ELSE " + CycleTime + " END * p.Operation_time)  * cap.Months as Capmonth " +
                            "FROM PMACS_Capacity_Press p " +
                            "INNER JOIN PMACS_ProdProcess cap ON cap.ProcessCode = p.ProcessCode " +
                            "LEFT JOIN PMACS_Forecast f ON f.Model_name = p.Model_name " +
                            "WHERE p.Capgroup_ID = " + capid + " AND p.IsDelete = 1 " +
                            "ORDER BY p.Capinfo_ID";

            return SqlDataAccess.GetData<PressModel>(strsql);
        }
        public async Task<bool> AddPressModels(PressModel press)
        {
            if (press == null) return false;

            string strsql;
            object parameter;

            if (press.Capgroup_ID == 10)
            {
                strsql = "INSERT INTO PMACS_Capacity_Press(Model_name, Product_type, SPM, Row, Lam, Operation_time) " +
                    "VALUES(@Model_name, @Product_type, @SPM, @Row, @Lam, @Operation_time)";
                parameter = new
                {
                    Model_name = press.Model_name,
                    Product_type = press.Product_type,
                    Lam = press.Lam,
                    SPM = press.SPM,
                    Row = press.Row,
                    Operation_time = press.Operation_time
                };
            }
            else if (press.Capgroup_ID == 11)
            {
                strsql = "INSERT INTO PMACS_Capacity_Press(Model_name, Product_type, CycleTime, Operation_time) " +
                    "VALUES(@Model_name, @Product_type, @CycleTime, @Operation_time)";

                parameter = new
                {
                    Model_name = press.Model_name,
                    Product_type = press.Product_type,
                    CycleTime = press.CycleTime,
                    Operation_time = press.Operation_time
                };
            }
            else
            {
                strsql = "INSERT INTO PMACS_Capacity_Press(Model_name, Product_type, Partnum, TotalCycle, Bucket, Cycle_cnc, Apearance_inspect,  Operation_time) " +
                   "VALUES(@Model_name, @Product_type, @Partnum, @TotalCycle, @Bucket, @Cycle_cnc, @Apearance_inspect, @Operation_time)";
                parameter = new
                {
                    Model_name = press.Model_name,
                    Partnum = press.Partnum,
                    Product_type = press.Product_type,
                    TotalCycle = press.TotalCycle,
                    Bucket = press.Bucket,
                    Cycle_cnc = press.Cycle_cnc,
                    Apearance_inspect = press.Apearance_inspect
                };
            }


            return await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }

        public async Task<bool> EditPressModels(PressModel press)
        {
            if (press == null) return false;

            string strsql = "";
            object parameter;

            if (press.Capgroup_ID == 10)
            {
                strsql = "UPDATE PMACS_Capacity_Press " +
                            "SET Row =@Row, Lam =@Lam, SPM =@SPM, Operation_time =@Operation_time " +
                            "WHERE  Capinfo_ID = @Capinfo_ID";
                parameter = new  { 
                    Lam = press.Lam, 
                    SPM = press.SPM, 
                    Row = press.Row,  
                    Operation_time = press.Operation_time, 
                    Capinfo_ID = press.Capinfo_ID
                };
            }
            else if (press.Capgroup_ID == 11)   
            {
                strsql = "UPDATE PMACS_Capacity_Press " +
                            "SET CycleTime =@CycleTime, Operation_time =@Operation_time " +
                            "WHERE  Capinfo_ID = @Capinfo_ID";
                parameter = new { 
                    CycleTime = press.CycleTime, 
                    Operation_time = press.Operation_time,  
                    Capinfo_ID = press.Capinfo_ID
                };
            }
            else
            {
                strsql = "UPDATE PMACS_Capacity_Press " +
                           "SET TotalCycle =@TotalCycle, Bucket =@Bucket,  Cycle_cnc =@Cycle_cnc, Apearance_inspect =@Apearance_inspect " +
                           "WHERE  Capinfo_ID = @Capinfo_ID";
                parameter = new { 
                    TotalCycle = press.TotalCycle, 
                    Bucket = press.Bucket, 
                    Cycle_cnc = press.Cycle_cnc, 
                    Apearance_inspect = press.Apearance_inspect, 
                    Capinfo_ID = press.Capinfo_ID 
                };
            }

            return await SqlDataAccess.UpdateInsertQuery(strsql, parameter);
        }
        // Rotor Section
        public Task<List<RotorModel>> GetRotorModels(string months, int capid)
        {
            //string month = months == "None" ? months : "";

            string strsql = "SELECT c.Capinfo_ID, COALESCE(c.Cover, '') as Cover, " +
                            "c.CycleTime, COALESCE(c.Impeller, '') as Impeller, " +
                            "c.Model_name, c.Dream, c.Operation_time, " +
                            "cp.Days, cp.Months, " +
                            "COALESCE(f." + months + ", 0) as foredata, c.Capgroup_ID as GroupID, c.ProcessCode, " +
                            "COALESCE(NULLIF(f." + months + " * c.CycleTime / 60 / 60, 0.0), 0.0)  as manhour, " +
                            "COALESCE(NULLIF(f." + months + " * c.CycleTime / 60 / 60 / 24 / c.Operation_time * 1.9, 0.0), 0.0)  as Require, " +
                            "COALESCE(NULLIF(f." + months + " * c.CycleTime / 60 / 60 / 24 / c.Operation_time * 1.9, 0.0), 0.0)  as Manpower,  " +
                            "(cp.Days * 3600 / c.CycleTime *  cp.OperationTime) as Capday, " +
                            "(cp.Days * 3600 / c.CycleTime  *  cp.OperationTime) * cp.Months as Capmonth " +
                            "FROM PMACS_Capacity_Rotor c " +
                            "LEFT JOIN PMACS_Forecast f ON f.Model_name = c.Model_name " +
                            "INNER JOIN PMACS_ProdProcess cp ON cp.ProcessCode = c.ProcessCode " +
                            "WHERE c.Capgroup_ID = @Capgroup_ID AND c.IsDelete = 1 " +
                            "ORDER BY c.Capinfo_ID";

            return SqlDataAccess.GetData<RotorModel>(strsql, new { Capgroup_ID = capid });
        }
        public Task<bool> AddRotorModels(RotorModel mold)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditRotorModels(RotorModel mold)
        {
            string strsql = "UPDATE PMACS_Capacity_Rotor " +
                            "SET CycleTime =@CycleTime, Cover =@Cover, Dream =@Dream, Impeller =@Impeller, Operation_time =@Operation_time " +
                            "WHERE  Capinfo_ID = @Capinfo_ID";

            return SqlDataAccess.UpdateInsertQuery(strsql, mold);
        }

        // Winding Section
        public Task<List<WindingModel>> GetWindingModels(string months, int capid)
        {
            string CycleTime = (capid == 1 || capid == 2) ? "(c.CycleTime / CASE WHEN c.Head_count = 0 THEN 1 ELSE c.Head_count END)" : "CASE WHEN c.CycleTime = 0 THEN 1 ELSE c.CycleTime END";
            //string month = months == "None" ? months : "";

            string strsql = $@"SELECT c.Capinfo_ID, c.Product_type, c.Model_name,c.Winding_Assy,
                        c.CycleTime, c.WireDia, c.WindTurns, c.jig, c.Head_count, cp.AvailMachine,
                        COALESCE(f.{months}, 0) as foredata, c.Capgroup_ID, c.ProcessCode,
                        COALESCE(NULLIF(f.{months} * {CycleTime} / 60 / 60, 0.0), 0.0)  as manhour,
                        COALESCE(NULLIF(f.{months} * {CycleTime} / 60 / 60 / 24 / c.Operation_time * 1.9, 0.0), 0.0)  as Require,
                        COALESCE(NULLIF(f.{months} * {CycleTime} / 60 / 60 / 24 / c.Operation_time * 1.9, 0.0), 0.0)  as Manpower, 
                        (cp.Days * 3600 / {CycleTime} *  cp.OperationTime) as Capday,
                        (cp.Days * 3600 / {CycleTime}  *  cp.OperationTime) * cp.Months as Capmonth
                        FROM PMACS_Capacity_Winding c
                        LEFT JOIN PMACS_Forecast f ON f.Model_name = c.Model_name
                        INNER JOIN PMACS_ProdProcess cp ON cp.ProcessCode = c.ProcessCode
                        WHERE c.Capgroup_ID = @Capgroup_ID  AND c.IsDelete = 1
                        ORDER BY c.Capinfo_ID";


            return SqlDataAccess.GetData<WindingModel>(strsql, new { Capgroup_ID = capid });
        }

        

        public Task<bool> EditWindingModelBase(WindingModel wind)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditWindingProcess(object parameters)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditWindingProducts(WindingModel wind)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> EditWindingDetails(WindingModel wind)
        {
            throw new System.NotImplementedException();
        }

       

        // Circuit Section
        public Task<List<CircuitModel>> GetCircuitModels(string months, int capid)
        {
            string strsql = "SELECT c.Capinfo_ID, c.CycleTime, " +
                                "c.Model_name, c.PWB_TYPE, c.Operation_time, c.Model, " +
                                "c.PCBA, c.PWB, c.PWB_BLOCKS, c.HUMISEAL, " + 
                                "c.PELGAN_Z, c.SUPER_XL, c.SILICONE, " +
                                "cap.AvailMachine, " +
                                "COALESCE(f." + months + ", 0) as foredata, c.ProcessCode, " +
                                "COALESCE(NULLIF(f." + months + " * COALESCE(c.CycleTime, 0.00) / 60 / 60, 0.0), 0.0)  as manhour, " +
                                "COALESCE(NULLIF(f." + months + " * COALESCE(c.CycleTime, 0.00) / 60 / 60 / 24 / c.Operation_time, 0.0), 0.0)  as Manpower, " +
                                "COALESCE(NULLIF(f." + months + " * COALESCE(c.CycleTime, 0.00) / 60 / 60 / 24 / c.Operation_time, 0.0), 0.0)  as Require, " +
                                "COALESCE((cap.Days * 3600 / CASE WHEN c.CycleTime = 0 THEN 1 ELSE c.CycleTime END / c.Operation_time), 0.0) as Capday, " +
                                "COALESCE((cap.Days * 3600 / CASE WHEN c.CycleTime = 0 THEN 1 ELSE c.CycleTime END / c.Operation_time)  * cap.Months, 0.0) as Capmonth " +
                            "FROM PMACS_Capacity_Circuit c " +
                            "INNER JOIN PMACS_ProdProcess cap ON cap.ProcessCode = c.ProcessCode " +
                            "LEFT JOIN PMACS_Forecast f ON f.Model_name = c.Model_name " +
                            "WHERE c.Capgroup_ID = " + capid + " " +
                            "ORDER BY c.Capinfo_ID";

            Debug.WriteLine(strsql);

            return SqlDataAccess.GetData<CircuitModel>(strsql);
        }



        // -------------------- UPDATE ALL FOR THE SUMMARY --------------------------------
        public  Task<bool> UpdateCapacityGroup(CapacityGroupPostModel cap)
        {
            string strsql = "UPDATE PMACS_ProdCapacity SET  Total_machine =@Total_machine, " +
                "Capday =@Capday, Capmonth =@Capmonth, TotalMan =@TotalMan " +
                "WHERE Capgroup_ID =@Capgroup_ID";
            return SqlDataAccess.UpdateInsertQuery(strsql, cap);   
        }
        public Task<bool> UpdateManpower(int totalManpower, string processcode)
        {
            string strsql = "UPDATE PMACS_ProdProcess SET RequireManpower =@RequireManpower WHERE ProcessCode =@ProcessCode";
            var parameters = new { RequireManpower = totalManpower, ProcessCode = processcode };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameters);
        }

        // Updated the Capacity Summary
        public Task<bool> EditP1SAsummary(PsummaryModel cap)
        {
            string strsql = "UPDATE PMACS_ProdProcess SET AvailMachine =@AvailMachine, " +
                "ActualMachine =@ActualMachine WHERE ProcessCode =@ProcessCode";
            var parameters = new { ProcessCode = cap.ProcessCode, AvailMachine = cap.AvailMachine, ActualMachine = cap.ActualMachine };
            return SqlDataAccess.UpdateInsertQuery(strsql, parameters, "p1sasummary");
        }

        public Task<bool> UpdateProcessform(ProcessformPostModel cap)
        {
            string strsql = "UPDATE PMACS_ProdProcess SET Days =@Days, Months =@Months, " +
                "OperationTime =@OperationTime, Cap_Per_Machine =@Cap_Per_Machine " +
                "WHERE ProcessCode =@ProcessCode";
            return SqlDataAccess.UpdateInsertQuery(strsql, cap);
        }


        public Task<bool> DeleteModels(int capinfo_id, int capgroup)
        {
            string strTable = "";
            switch (capgroup)
            {
                case 1:
                case 2:
                case 3:
                    strTable = "PMACS_Capacity_Winding";
                    break;
                case 4:
                case 5:
                case 6: 
                    strTable = "PMACS_Capacity_Rotor";
                    break;
                case 7:
                case 8:
                case 9:
                    strTable = "PMACS_Capacity_Molding";
                    break;
                case 10:
                case 11:
                case 12:
                    strTable = "PMACS_Capacity_Press";
                    break;
                default: 
                    strTable = "PMACS_Capacity_Circuit";
                    break;
            }

            string strquery = "UPDATE " + strTable + " SET IsDelete = 0  WHERE Capinfo_ID =@Capinfo_ID ";
            return SqlDataAccess.UpdateInsertQuery(strquery, new { Capinfo_ID = capinfo_id });
        }

        public Task<bool> EditWindingProducts(object parameter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateForecast(forecastInput fores, string strsql, string[] columns)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@forest_code", 0);
            int count = 0;

            foreach (var column in columns)
            {
                switch (count)
                {
                    case 0:        
                        parameters.Add("@" + column, fores.column2);
                        break;
                    case 1:
                        parameters.Add("@" + column, fores.column3);
                        break;
                    case 2:
                        parameters.Add("@" + column, fores.column4);
                        break;
                    case 3:
                        parameters.Add("@" + column, fores.column5);
                        break;
                    case 4:
                        parameters.Add("@" + column, fores.column6);
                        break;
                    case 5:
                        parameters.Add("@" + column, fores.column7);
                        break;
                    case 6:
                        parameters.Add("@" + column, fores.column8);
                        break;
                }
                count++;
            }

            return SqlDataAccess.UpdateInsertQuery(strsql, parameters);
        }
    }
}