using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Model;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<List<SummaryCount>> GetTotalSummary(DateTime testDate, List<string> prefixes)
        {
           
            var conditions = new List<string>();
            var parameters = new Dictionary<string, object>();
            
            parameters.Add("@TestDate", testDate.Date);


            if (prefixes != null && prefixes.Any())
            {
                for (int i = 0; i < prefixes.Count; i++)
                {
                    string paramName = "@p" + i;
                    conditions.Add($"EmployeeID LIKE {paramName}");
                    parameters.Add(paramName, prefixes[i] + "%");
                }
            }

            string sql = $@"
                SELECT
                    ISNULL(SUM(CASE WHEN ComprehensiveResult = 1 THEN 1 ELSE 0 END), 0) AS PassCount,
                    ISNULL(SUM(CASE WHEN ComprehensiveResult = 0 THEN 1 ELSE 0 END), 0) AS FailCount
                FROM FootWristStrapTestResults
                WHERE CAST(TestDate AS date) = @TestDate
                {(conditions.Any() ? "AND (" + string.Join(" OR ", conditions) + ")" : "")}
            ";

            Debug.WriteLine(sql);

            return await SqlDataAccess.GetData<SummaryCount>(sql, parameters);
        }

        public Task<bool> ImportSetFootAnalysis(IFootWristModel foot)
        {
            string sql = @"
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


            //string sql = @"
            //    IF NOT EXISTS
            //    (
            //        SELECT 1
            //        FROM FootWristStrapTestResults
            //        WHERE EmployeeID = @EmployeeID
            //          AND CAST(TestDate AS DATE) = CAST(@TestDate AS DATE)
            //    )
            //    BEGIN
            //        INSERT INTO FootWristStrapTestResults
            //        (
            //            TestDate,
            //            TestTime,
            //            EmployeeID,
            //            EmployeeName,
            //            ComprehensiveResult,
            //            LeftFootResistance,
            //            LeftFootResult,
            //            RightFootResistance,
            //            RightFootResult,
            //            WristStrapResult,
            //            ConductivityEvaluation,
            //            LowerEvaluationLimit,
            //            UpperEvaluationLimit,
            //            EvaluationBuzzer,
            //            EvaluationExternalOutput,
            //            FG470,
            //            Note
            //        )
            //        VALUES
            //        (
            //            @TestDate,
            //            @TestTime,
            //            @EmployeeID,
            //            @EmployeeName,
            //            @ComprehensiveResult,
            //            @LeftFootResistance,
            //            @LeftFootResult,
            //            @RightFootResistance,
            //            @RightFootResult,
            //            @WristStrapResult,
            //            @ConductivityEvaluation,
            //            @LowerEvaluationLimit,
            //            @UpperEvaluationLimit,
            //            @EvaluationBuzzer,
            //            @EvaluationExternalOutput,
            //            @FG470,
            //            @Note
            //        );
            //    END";

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

        public async Task<bool> DeleteByTestDate(DateTime testDate)
        {
            string sql = @"
                DELETE FROM FootWristStrapTestResults
                WHERE TestDate >= CAST(@StartDate AS DATE)
                  AND TestDate < DATEADD(DAY, 1, @StartDate);
            ";

            return await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                StartDate = testDate.Date
            });
        }

        public Task<int> GetRowCountByDate(string testDate)
        {
            string sql = @"SELECT COUNT(*) 
                   FROM FootWristStrapTestResults 
                   WHERE  CAST(TestDate AS DATE)  = CAST(@TestDate AS DATE)";

            return  SqlDataAccess.GetCountData(
                sql,
                new { TestDate = testDate }
            );
        }

    
    }
}
