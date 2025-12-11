using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootWristStrapsAnalysis.Services
{
    internal class FootWristServices : IFootWrist
    {
        public async Task<IEnumerable<IFootWristModel>> GetFootAnalysisData()
        {
            string strsql = $@"SELECT RecordID
                              ,TestDate
                              ,TestTime
                              ,EmployeeID
                              ,EmployeeName
                              ,ComprehensiveResult
                              ,LeftFootResistance
                              ,LeftFootResult
                              ,RightFootResistance
                              ,RightFootResult
                              ,WristStrapResult
                              ,ConductivityEvaluation
                              ,LowerEvaluationLimit
                              ,UpperEvaluationLimit
                              ,EvaluationBuzzer
                              ,EvaluationExternalOutput
                              ,FG470
                              ,Note
                          FROM FootWristStrapTestResults";

            return await SqlDataAccess.GetIEnumerableData<IFootWristModel>(strsql);
        }
        public async Task<IEnumerable<IFootWristModel>> GetTestDataForMonth(int month, int year)
        {
            string strsql = $@"SELECT 
                                TestDate,
                                EmployeeID,
                                EmployeeName,
                                LeftFootResistance,
                                LeftFootResult,
                                RightFootResistance,
                                RightFootResult,
                                ComprehensiveResult
                            FROM FootWristStrapTestResults
                            WHERE MONTH(TestDate) ={month} AND YEAR(TestDate) ={year}
                            ORDER BY TestDate, EmployeeID";
            return await SqlDataAccess.GetIEnumerableData<IFootWristModel>(strsql, null);
        }


        public Task<bool> ImportSetFootAnalysis(IFootWristModel foot)
        {
            string insertQuery = @"
                    INSERT INTO FootWristStrapTestResults
                    (
                        TestDate,
                        TestTime,
                        EmployeeID,
                        EmployeeName,
                        ComprehensiveResult,
                        LeftFootResistance,
                        LeftFootResult,
                        RightFootResistance,
                        RightFootResult,
                        WristStrapResult,
                        ConductivityEvaluation,
                        LowerEvaluationLimit,
                        UpperEvaluationLimit,
                        EvaluationBuzzer,
                        EvaluationExternalOutput,
                        FG470,
                        Note
                    )
                    VALUES
                    (
                        @TestDate,
                        @TestTime,
                        @EmployeeID,
                        @EmployeeName,
                        @ComprehensiveResult,
                        @LeftFootResistance,
                        @LeftFootResult,
                        @RightFootResistance,
                        @RightFootResult,
                        @WristStrapResult,
                        @ConductivityEvaluation,
                        @LowerEvaluationLimit,
                        @UpperEvaluationLimit,
                        @EvaluationBuzzer,
                        @EvaluationExternalOutput,
                        @FG470,
                        @Note
                    );";

             return SqlDataAccess.UpdateInsertQuery(insertQuery, foot);
        }

        public Task<bool> CheckIfEmployeeIDImportToday(string EmployeeID, DateTime today)
        {
            string checkdata = $@"SELECT COUNT(*) AS ExistingCount
                            FROM FootWristStrapTestResults
                            WHERE EmployeeID = @EmployeeID
                              AND CAST(TestDate AS DATE) = CAST(GETDATE() AS DATE);";
            return SqlDataAccess.Checkdata(checkdata, new { EmployeeID });
        }

        public Task<bool> CheckIfEmployeeIDImportPrevious(string EmployeeID, DateTime today)
        {
            string checkdata = @"
                    SELECT COUNT(*) AS ExistingCount
                    FROM FootWristStrapTestResults
                    WHERE EmployeeID = @EmployeeID
                      AND CAST(TestDate AS DATE) = CAST(@Date AS DATE);";

            return SqlDataAccess.Checkdata(checkdata, new { EmployeeID, Date = today });
        }

       
    }
}
