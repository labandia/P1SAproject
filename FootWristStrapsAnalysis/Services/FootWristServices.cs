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
            string sql = @"
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM FootWristStrapTestResults
                    WHERE EmployeeID = @EmployeeID
                      AND CAST(TestDate AS DATE) = CAST(@TestDate AS DATE)
                )
                BEGIN
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
                    );
                END";

            return SqlDataAccess.UpdateInsertQuery(sql, foot);
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

        public Task<List<string>> GetSelectedEmployeeID(int month, int year)
        {
            string query = @"
                        SELECT DISTINCT EmployeeID
                        FROM FootWristStrapTestResults
                        WHERE MONTH(TestDate) = @Month AND YEAR(TestDate) = @Year
                        ORDER BY EmployeeID";

            return SqlDataAccess.StringList(query, new { Month = month, Year = year });
        }
    }
}
