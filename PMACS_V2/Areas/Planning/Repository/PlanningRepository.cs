using PMACS_V2.Areas.Planning.Interface;
using PMACS_V2.Areas.Planning.Model;
using PMACS_V2.Helper;
using ProgramPartListWeb.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;

namespace PMACS_V2.Areas.Planning.Repository
{
    public class PlanningRepository : IPlanning
    {
        public string checkDatenow = DateTime.Now.ToString("yyyy-MM-dd");

        //------------- GET DATA FUNCTION ----------------------------
        public async Task<DataTable> AdditionalSummary(string dstart, string dend)
        {
            var columns = new List<string>();
            var dates = await GetcolumnDates(dstart, dend);
            DataTable result;

            if (dates.Rows.Count > 0)
            {
                foreach (DataRow row in dates.Rows)
                {
                    columns.Add(row["dates"].ToString());
                }

                var formattedColumns = columns.Select(res => $"COALESCE([{res}], 0) AS '{res.Substring(5)}'").ToArray();
                var formattedOrders = columns.Select(res => $"[{res}]").ToArray();
                var joinedColumns = string.Join(", ", formattedColumns);
                var joinOrders = string.Join(", ", formattedOrders);

                var strquery = $"SELECT Imports, {joinedColumns} " +
                              "FROM (SELECT Imports, CAST(DateImport AS DATE) AS DateImport, TotalQuan " +
                              "FROM M1_Lacking_table) AS SourceTable " +
                              $"PIVOT (SUM(TotalQuan) FOR DateImport IN ({joinOrders})) AS PivotTable;";

                result = await SqlDataAccess.GetDataByDataTable(strquery);
            }
            else
            {
                result = null;
            }

            return result;
        }

        public async Task<DataTable> LackingResultSummary(string dstart, string dend)
        {
            var columns = new List<string>();
            var dates = await GetcolumnDates(dstart, dend);
            DataTable result;

            if (dates.Rows.Count > 0)
            {
                foreach (DataRow row in dates.Rows)
                {
                    columns.Add(row["dates"].ToString());
                }

                var formattedColumns = columns.Select(res =>
                {
                    if (DateTime.TryParse(res, out var parsedDate))
                    {
                        return $"COALESCE([{res}], 0) AS '{parsedDate.ToString("MM/dd/yyyy")}'";
                    }
                    return $"COALESCE([{res}], 0) AS '{res}'";
                }).ToArray();

                var formattedOrders = columns.Select(res => $"[{res}]").ToArray();
                var joinedColumns = string.Join(", ", formattedColumns);
                var joinOrders = string.Join(", ", formattedOrders);

                var strquery = $"SELECT Imports, {joinedColumns} " +
                              "FROM (SELECT Imports, CAST(Datetoday AS DATE) AS DateImport, LackTotal " +
                              "FROM M1_Lacking_Summary) AS SourceTable " +
                              $"PIVOT (SUM(LackTotal) FOR DateImport IN ({joinOrders})) AS PivotTable;";

                result = await SqlDataAccess.GetDataByDataTable(strquery);
            }
            else
            {
                result = null;
            }
            return result;
        }

        public async Task<DataTable> ShopOrderSummary(string dstart, string dend)
        {
            var columns = new List<string>();
            var dates = await GetcolumnDates(dstart, dend);
            DataTable result;

            if (dates.Rows.Count > 0)
            {
                foreach (DataRow row in dates.Rows)
                {
                    columns.Add(row["dates"].ToString());
                }

                var formattedColumns = columns.Select(res =>
                {
                    if (DateTime.TryParse(res, out var parsedDate))
                    {
                        return $"COALESCE([{res}], 0) AS '{parsedDate.ToString("MM/dd/yyyy")}'";
                    }
                    return $"COALESCE([{res}], 0) AS '{res}'";
                }).ToArray();

                var formattedOrders = columns.Select(res => $"[{res}]").ToArray();
                var joinedColumns = string.Join(", ", formattedColumns);
                var joinOrders = string.Join(", ", formattedOrders);

                var strquery = "SELECT Imports, " + joinedColumns + " " +
                              "FROM (SELECT Imports, CAST(Datetoday AS DATE) AS DateImport, ShopCount " +
                              "FROM M1_DailyOrder_Summary) AS SourceTable " +
                              $"PIVOT (SUM(ShopCount) FOR DateImport IN ({joinOrders})) AS PivotTable;";

                result = await SqlDataAccess.GetDataByDataTable(strquery);
            }
            else
            {
                result = null;
            }

            return result;
        }

        public async Task<DataTable> GetSalesRequestSummary()
        {
            var monthYearList = new List<SalesMonthInfo>();
            var dates = await GetSalesMonths(DateTime.Now.ToString("yyyy"));

            foreach (DataRow row in dates.Rows)
            {
                monthYearList.Add(new SalesMonthInfo
                {
                    MonthAbbreviation = row["SalesRequest"].ToString(),
                    Year = row["DateYearupload"].ToString()
                });
            }

            monthYearList = monthYearList
                .GroupBy(m => m.MonthAbbreviation + m.Year)
                .Select(g => g.First())
                .OrderBy(m => Convert.ToInt32(m.Year))
                .ToList();

            var formattedColumns = monthYearList
                .Select(m => $"CONCAT(COALESCE([{m.MonthAbbreviation}], 0), ':', MonthUpload) AS '{m.MonthAbbreviation} / {m.Year}'")
                .ToArray();

            var formattedOrders = monthYearList
                .Select(m => $"[{m.MonthAbbreviation}]")
                .Distinct()
                .ToArray();

            var joinedColumns = string.Join(", ", formattedColumns);
            var joinOrders = string.Join(", ", formattedOrders);

            var strquery = "SELECT MonthUpload, " + joinedColumns + " " +
                          "FROM (SELECT MonthUpload, SalesRequest AS SalesMonth, TotalCountOrder " +
                          "FROM M1_RequestSales_table) AS SourceTable " +
                          $"PIVOT (SUM(TotalCountOrder) FOR SalesMonth IN ({joinOrders})) AS PivotTable;";

            var result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

        //----------------- Partnumber summary tabs ------------------------
        public async Task<DataTable> GetDailyPartnumberSummary(string dstart, string dend)
        {
            var columns = new List<string>();
            var dates = await GetcolumnDates(dstart, dend);

            foreach (DataRow row in dates.Rows)
            {
                columns.Add(row["dates"].ToString());
            }

            var formattedColumns = columns.Select(res =>
            {
                if (DateTime.TryParse(res, out var parsedDate))
                {
                    return $"COALESCE([{res}], 0) AS '{parsedDate.ToString("MM/dd/yyyy")}'";
                }
                return $"COALESCE([{res}], 0) AS '{res}'";
            }).ToArray();

            var joinedColumns = string.Join(", ", formattedColumns);
            var formattedOrders = columns.Select(res => $"[{res}]").ToArray();
            var joinOrders = string.Join(", ", formattedOrders);

            var strquery = "SELECT Partnum, Partname, " + joinedColumns + " " +
                          "FROM (SELECT CONVERT(VARCHAR, DateImport, 111) AS Dateupload, " +
                          "Partnum, Partname, TotalQuan FROM M1_Lacking_table) AS SourceTable " +
                          $"PIVOT (MAX(TotalQuan) FOR Dateupload IN ({joinOrders})) AS PivotTable " +
                          $"ORDER BY {joinOrders} DESC;";

            var result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

        //----------------- Display the Uploaded data ------------------------
        public async Task<List<Planningmodel>> GetPCDataList(int intsize, int intnum)
        {
            var strquery = "GetPCData";
            var parameter = new { PageSize = intsize, PageNumber = intnum };
            var result = await SqlDataAccess.GetData<Planningmodel>(strquery, parameter);
            return result;
        }

        public async Task<List<BranchModel>> GetBranchSummary()
        {
            var strquery = "BranchDisplay";
            var result = await SqlDataAccess.GetData<BranchModel>(strquery);
            return result;
        }

        //----------------- Display the EndMonth data ------------------------
        public async Task<List<EndMonthModel>> GetEndMonthlist(string stryear)
        {
            var strquery = "EndMonthQuery";
            var result = await SqlDataAccess.GetData<EndMonthModel>(strquery, new { EndYear = stryear });
            return result;
        }

        //------------- GET DATA WHEN THE USERS CLICKS THE ROW DATA ----------------------------
        public async Task<List<ProductsModel>> GetSelectedDetailsSummary(string strdate, int importID)
        {
            var strquery = "SelectedDetails";
            var parameter = new { Dateslected = strdate, ImportsID = importID };
            var result = await SqlDataAccess.GetData<ProductsModel>(strquery, parameter);
            return result;
        }

        public async Task<List<ProductsModel>> GetSelectedlackDetailsSummary(string strdate, int importID)
        {
            var strquery = "SELECT Partnum as Partnumber, Partname, TotalQuan as Totalpart " +
                          "FROM M1_Lacking_table " +
                          "WHERE CAST(DateImport AS DATE) = @Dateslected " +
                          $"AND Imports IN ({importID})";

            var parameter = new { Dateslected = strdate };
            var result = await SqlDataAccess.GetData<ProductsModel>(strquery, parameter);
            return result;
        }

        public async Task<List<ShopOrderResultModel>> GetSelectedShopOrderDetailsSummary(string strdate, int importID)
        {
            var strquery = "SelectedShopOrderDetails";
            var parameter = new { Dateslected = strdate, ImportsID = importID };
            var result = await SqlDataAccess.GetData<ShopOrderResultModel>(strquery, parameter);
            return result;
        }

        public async Task<List<ShopOrderResultModel>> GetSelectedRequestsDetailsSummary(int colint, int rowint, int yearint)
        {
            Debug.WriteLine($"HERE  Month: {colint}, rowMonth : {rowint} ");
            var strquery = "SelectedRequestDetails";
            var parameter = new { DateUpload = rowint, DateSales = colint };
            var result = await SqlDataAccess.GetData<ShopOrderResultModel>(strquery, parameter);
            return result;
        }

        //----------------- Partnumber summary tabs when Selecting a row data ------------------------
        public async Task<DataTable> GetSelectedPartnumberSummary(string dstart)
        {
            var strquery = $"SELECT Partnum, COALESCE([{dstart}], 0) AS '{dstart}' " +
                          $"FROM (SELECT CONVERT(VARCHAR, DateImport, 111) AS Dateupload, Partnum, TotalQuan " +
                          $"FROM M1_Lacking_table WHERE CAST(DateImport AS DATE) = '{dstart}') AS SourceTable " +
                          $"PIVOT (MAX(TotalQuan) FOR Dateupload IN ([{dstart}])) AS PivotTable " +
                          $"ORDER BY [{dstart}] DESC;";

            var result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

        //----------------- Check if the Excel data is already exist in the database today  ------------------------
        public async Task<int> ChecksDailyImport(string DateNowString)
        {
            var strquery = "SELECT COUNT(DateUpload) FROM M1_Main_table WHERE CAST(DateUpload AS DATE) = @Datenow";
            var parameter = new { Datenow = DateNowString };
            var count = await SqlDataAccess.GetCountData(strquery, parameter);
            return count;
        }

        //###########################################  ALL IMPORTS DATA ARE ALL HERE ##################################################
        public async Task<bool> AddEndMonthData(object parameters)
        {
            var strquery = "INSERT INTO M1_Monthly_Table(DateMonth, EndTotalOrders, EndRemainOrders, CurrentTotalOrders, CurrentRemains, EndYear) " +
                          "VALUES (@DateMonth, @EndTotalOrders, @EndRemainOrders, @CurrentTotalOrders, @CurrentRemains, @EndYear)";

            var result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }

        public async Task<bool> UpdateEndMonthData(object parameters)
        {
            var strquery = "UPDATE M1_Monthly_Table SET EndTotalOrders = @EndTotalOrders, EndRemainOrders = @EndRemainOrders, " +
                          "CurrentTotalOrders = @CurrentTotalOrders, CurrentRemains = @CurrentRemains WHERE RecordID = @RecordID";
            var result = await SqlDataAccess.UpdateInsertQuery(strquery, parameters);
            return result;
        }

        //------------- OTHER FUNCTION CONNECTED ----------------------------
        public async Task<DataTable> GetcolumnDates(string dstart, string dend)
        {
            var strquery = $"SELECT FORMAT(CAST(DateUpload AS DATE), 'yyyy/MM/dd') AS dates " +
                          "FROM M1_Main_table " +
                          $"WHERE DateUpload BETWEEN '{dstart}' AND '{dend}' " +
                          "GROUP BY CAST(DateUpload AS DATE) " +
                          "ORDER BY CAST(DateUpload AS DATE) ASC";

            var result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

        public async Task<DataTable> GetSalesMonths(string stryear)
        {
            //var strquery = "SELECT SalesRequest, MonthUpload, DateYearupload " +
            //              "FROM M1_RequestSales_table " +
            //              "ORDER BY SalesRequest, DateYearupload ASC";
            var strquery = "SELECT SalesRequest, DateYearupload " +
                           "FROM M1_RequestSales_table " +
                           "GROUP BY SalesRequest, DateYearupload ";

            var result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

        public async Task<int> GetPartnumberExist(string strpartnum)
        {
            string Condition = "";
            object paramsObj;

            if (!string.IsNullOrEmpty(strpartnum))
            {
                Condition = "WHERE Partnum = @Partnum";
                paramsObj = new { Partnum = strpartnum };
            }
            else
            {
                paramsObj = null;
            }

            var strquery = $"SELECT COALESCE(MAX(Imports), 0) FROM M1_Lacking_table {Condition}";
            var count = await SqlDataAccess.GetCountData(strquery, paramsObj);
            return count;
        }

        public async Task<bool> CheckEndMonthExist(string strdate)
        {
            var checkram = await SqlDataAccess.Checkdata(
                "SELECT ISNULL(COUNT(DateMonth), 0) FROM M1_Monthly_Table WHERE DateMonth = @DateMonth",
                new { DateMonth = strdate });
            return checkram;
        }

        public async Task<List<ProductsModel>> GetProductypeByDay(string strdate)
        {
            var strquery = "GetproductSummary";
            var parameter = new { SelectDate = strdate };
            var result = await SqlDataAccess.GetData<ProductsModel>(strquery, parameter);
            return result;
        }

        public async Task Insertproduct(ProductsModel prod, int count)
        {
            var checkram = await SqlDataAccess.Checkdata(
                "SELECT ISNULL(COUNT(Partnum), 0) FROM M1_Lacking_table WHERE Partnum = @Partnum",
                new { Partnum = prod.Partnumber });

            if (!checkram)
            {
                var strquery = "INSERT INTO M1_Lacking_table(Partnum, Partname, TotalQuan, Imports, Addition) " +
                              "VALUES (@Partnum, @Partname, @TotalQuan, @Imports, @Addition)";

                var insertquery = new
                {
                    Partnum = prod.Partnumber,
                    Partname = prod.Partname,
                    TotalQuan = prod.Totalpart,
                    Imports = count,
                    Addition = 1
                };
                await SqlDataAccess.UpdateInsertQuery(strquery, insertquery);
            }
            else
            {
                var strquery = "INSERT INTO M1_Lacking_table(Partnum, Partname, TotalQuan, Imports, Addition) " +
                              "VALUES (@Partnum, @Partname, @TotalQuan, @Imports, @Addition)";

                var count2 = await GetPartnumberExist(prod.Partnumber);
                var insertquery = new
                {
                    Partnum = prod.Partnumber,
                    Partname = prod.Partname,
                    TotalQuan = prod.Totalpart,
                    Imports = count2,
                    Addition = 0
                };
                await SqlDataAccess.UpdateInsertQuery(strquery, insertquery);
            }
        }

        public async Task<bool> InsertM1Result(DataTable dt)
        {
            var result = await SqlDataAccess.UploadDataFiles(dt, 0);
            return result;
        }

        public async Task<bool> InsertDataExcelFile(List<M1ExcelData> dt, string timecheck)
        {
            await ResetsUploadData();
            Debug.WriteLine("HERE");


            await UploadMainTable(dt);
            await UploadSummaryandTable(dt);
            await UpdateSalesRequestSummary(dt);
            await UpdateMonthlySales();

            //var uploadTasks = new List<Task>
            //{
            //    Task.Run(() => UploadSummaryandTable(dt)),
            //    Task.Run(() => UpdateSalesRequestSummaryV2(dt))
            //};

            //await Task.WhenAll(uploadTasks);
            return true;
        }

        public async Task UploadMainTable(List<M1ExcelData> dt)
        {
            var uniqueDates = dt.Distinct().OrderBy(d => d.DateUpload).ToList();

            try
            {
                foreach (var item in uniqueDates)
                {
                    //var formattedDateUpload = item.DateUpload.ToString("yyyy-MM-dd");

                    var formattedDateUpload = item.DateUpload.ToString("yyyy-MM-dd");
                    var strlacktable = "INSERT INTO M1_Main_table(DateUpload, DOI_Date, Date_Created, Branch, MD_Request, " +
                                      "SDP_Shoporder, SDP_Sales_Partnum, SDP_Sales_Number, Sales_Request_Date, Previous_Update, " +
                                      "PC_Proposed_Date, M1_Latest_Update, PC_Latest_Proposed_Date, Status, Partnumber, " +
                                      "Partname, Usage, Judgement) " +
                                      "VALUES(@DateUpload, @DOI_Date, @Date_Created, @Branch, @MD_Request, @SDP_Shoporder, " +
                                      "@SDP_Sales_Partnum, @SDP_Sales_Number, @Sales_Request_Date, @Previous_Update, " +
                                      "@PC_Proposed_Date, @M1_Latest_Update, @PC_Latest_Proposed_Date, @Status, @Partnumber, " +
                                      "@Partname, @Usage, @Judgement)";

                    var lacktableparam = new
                    {
                        DateUpload = formattedDateUpload,
                        DOI_Date = item.DOItoM1,
                        Date_Created = item.CreatedDate,
                        Branch = item.Branch,
                        MD_Request = item.MDDate,
                        SDP_Shoporder = item.SDPOrderNo,
                        SDP_Sales_Partnum = item.SDPSalesPartNo,
                        SDP_Sales_Number = item.SDPSalesQty,
                        Sales_Request_Date = item.SalesRequestedShipDate,
                        Previous_Update = item.PreviousUpdate,
                        PC_Proposed_Date = item.PC1LatestProposedShipDate,
                        M1_Latest_Update = item.M1LatestUpdate,
                        PC_Latest_Proposed_Date = item.PC1LatestProposedShipDate,
                        Status = item.ReplyStatus,
                        Partnumber = item.PartNo,
                        Partname = item.PartName,
                        Usage = item.Usage,
                        Judgement = item.Judgement
                    };

                    await SqlDataAccess.UpdateInsertQuery(strlacktable, lacktableparam);
                    await Task.Delay(500);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task UploadSummaryandTable(List<M1ExcelData> dt)
        {
            var formats = new[] { "M/d/yy", "MM/dd/yy", "dd/MM/yyyy" };
            var culture = CultureInfo.InvariantCulture;

            var uniqueDates = dt
                     .Select(o => o.DateUpload.Date) // Only take the date part
                     .Distinct()
                     .OrderBy(d => d)
                     .ToList();

            //foreach (var item in uniqueDates)
            //{
            //    Debug.WriteLine("DateUpload " + item);
            //}

            for (int i = 0; i < uniqueDates.Count; i++)
            {
                var sampleupload = uniqueDates[i];
                var parsedDate = DateTime.Parse(sampleupload.ToString());
                var formattedDate = parsedDate.ToString("MM/dd/yyyy");
                for (int j = 0; j <= i; j++)
                {
                    var currentDate = uniqueDates[j];
                    var intimports = j + 1;

                    var groupedData = dt
                        .Where(o => o.DateUpload.Date == currentDate.Date)
                        .GroupBy(o => o.PartNo)
                        .Select(g => new
                        {
                            Partnum = g.Key,
                            DateUpload = g.First().DateUpload,
                            Partnumber = g.Key,
                            Partsname = g.First().PartName,
                            NeedQty = g.First().NeedQty,
                            TotalQty = g.Sum(o => o.NeedQty),
                            Imports = intimports
                        }).ToList();

                    // GET THE TOTAL QUANTITY OF NEEDSQTY
                    double totalQty = groupedData.Sum(o => o.NeedQty);

                    string strlacksummary = "INSERT INTO M1_Lacking_Summary(Datetoday, LackTotal, Imports) " +
                          "VALUES(@Datetoday, @LackTotal, @Imports)";

                    var lackparam = new
                    {
                        Datetoday = formattedDate,
                        LackTotal = totalQty,
                        Imports = intimports
                    };

                    await SqlDataAccess.UpdateInsertQuery(strlacksummary, lackparam);

                    foreach (var item in groupedData)
                    {
                        var strlacktable = "INSERT INTO M1_Lacking_table(DateImport, Partnum, Partname, TotalQuan, Imports) " +
                                           "VALUES(@DateImport, @Partnum, @Partname, @TotalQuan, @Imports)";
                        var lacktableparam = new
                        {
                            DateImport = formattedDate,
                            Partnum = item.Partnumber,
                            Partname = item.Partsname,
                            TotalQuan = item.TotalQty,
                            Imports = intimports
                        };
                        //Debug.WriteLine($"No.  Date Import : {formattedDate}, Partnumber : {item.Partnumber} - Part name : {item.Partsname}, Imports : {intimports}");
                        await SqlDataAccess.UpdateInsertQuery(strlacktable, lacktableparam);
                    }
                    // ===========================================================
                    // ==================== SHOP ORDER DATA  =====================
                    // ===========================================================

                    var groupedSalesData = dt
                        .Where(o => o.DateUpload.Date == currentDate.Date)
                        .GroupBy(o => o.SDPOrderNo)
                        .Select(g => new
                        {
                            SDPOrderNo = g.Key,
                            Branches = g.First().Branch,
                            DateImport = g.First().DateUpload,
                            SDP_Shoporder = g.First().SDPOrderNo,
                            SDP_Sales_Partnum = g.First().SDPSalesPartNo,
                            SDP_Sales_Number = g.First().SDPSalesQty,
                            Sales_Request_Date = g.First().SalesRequestedShipDate,
                            PC_Proposed_Date = g.First().PC1ProposedShipDate,
                            Imports = intimports
                        }).Distinct().OrderBy(o => o.DateImport).ToList();

                    int SalesTotalCount = groupedSalesData.Count();

                    string strordersum = "INSERT INTO M1_DailyOrder_Summary(Datetoday, ShopCount, Imports) " +
                                      "VALUES(@Datetoday, @ShopCount, @Imports)";

                    var orderparam = new
                    {
                        Datetoday = formattedDate,
                        ShopCount = SalesTotalCount,
                        Imports = intimports
                    };

                    await SqlDataAccess.UpdateInsertQuery(strordersum, orderparam);

                    foreach (var item in groupedSalesData)
                    {
                        var strorderTable = "INSERT INTO M1_DailyOrder_Table(DateImport, Branch, SDP_Shoporder,  PC_Proposed_Date, SDP_Sales_Partnum, " +
                                       "SDP_Sales_Number, Sales_Request_Date, Imports) " +
                                       "VALUES(@DateImport, @Branch, @SDP_Shoporder, @PC_Proposed_Date, @SDP_Sales_Partnum, " +
                                       "@SDP_Sales_Number,  @Sales_Request_Date, @Imports)";
                        var orderTableparam = new
                        {
                            DateImport = formattedDate,
                            Branch = item.Branches,
                            SDP_Shoporder = item.SDP_Shoporder,
                            PC_Proposed_Date = item.PC_Proposed_Date,
                            SDP_Sales_Partnum = item.SDP_Sales_Partnum,
                            SDP_Sales_Number = item.SDP_Sales_Number,
                            Sales_Request_Date = item.Sales_Request_Date,
                            Imports = intimports
                        };
                        Debug.WriteLine(orderTableparam);

                        //Debug.WriteLine($"Date Import : {item.Branches}, SDP_Sales_Numbers: {item.SDP_Sales_Number}, SDP_Sales_Numbers: {item.SDP_Sales_Partnum}");
                        await SqlDataAccess.UpdateInsertQuery(strorderTable, orderTableparam);
                    }


                }
            }
            await Task.Delay(500);
        }

        public async Task UpdateSalesRequestSummary(List<M1ExcelData> dt)
        {
            var uniqueDates = dt.Select(o => o.DateUpload).Distinct().OrderBy(d => d).ToList();

            // GET THE CURRENT DATE AMND YEAR
            var Datetoday = DateTime.Now.Date.ToString("dd/MM/yyyy");
            var todayparts = Datetoday.Split('/');
            var todayear = todayparts[2];

            var groupedSalesData = dt.GroupBy(o => o.No)
                    .Select(g => new
                    {
                        Dateupload = g.First().DateUpload,
                        Sales_Request_Date = g.First().SalesRequestedShipDate,
                        DOItoM1 = g.First().DOItoM1,
                        SDP_Shoporder = g.First().SDPOrderNo,
                        Branches = g.First().Branch,
                        DateImport = g.First().DateUpload,
                        SDP_Sales_Partnum = g.First().SDPSalesPartNo,
                        SDP_Sales_Number = g.First().SDPSalesQty,
                        PC_Proposed_Date = g.First().PC1ProposedShipDate
                    }).Distinct().ToList();

            foreach (var item in groupedSalesData)
            {
                var DateUpload = item.Dateupload.ToString("MM/dd/yyyy");
                var UploadParts = DateUpload.Split('/');
                var UploadFullyear = UploadParts[2];

                var salesDate = item.Sales_Request_Date.ToString("MM/dd/yyyy");
                var dateParts = salesDate.Split('/');
                var Fullyear = dateParts[2];

                var strquery = "SELECT DISTINCT COALESCE(TotalCountOrder, 0) as TotalCountOrder " +
                         "FROM M1_RequestSales_table " +
                         "WHERE SalesRequest = @SalesRequest " +
                         "AND MonthUpload = @MonthUpload";
                var paramsObj = new
                {
                    SalesRequest = Convert.ToInt32(dateParts[0]),
                    MonthUpload = Convert.ToInt32(UploadParts[0])
                };

                int CurrentCount = await SqlDataAccess.GetCountData(strquery, paramsObj);
                //Debug.WriteLine("COUNT : " + CurrentCount);
                if (CurrentCount == 0)
                {
                    //Debug.WriteLine($"Date Upload : {DateUpload}  -  INserted ");
                    var insertquery = "INSERT INTO M1_RequestSales_table(MonthUpload, SalesRequest, " +
                                    "TotalCountOrder, DateYearupload, TodayYearupload) " +
                                    "VALUES(@MonthUpload, @SalesRequest, @TotalCountOrder, @DateYearupload, @TodayYearupload)";
                    var insertObj = new
                    {
                        SalesRequest = Convert.ToInt32(dateParts[0]),
                        MonthUpload = Convert.ToInt32(UploadParts[0]),
                        DateYearupload = dateParts[2],
                        TotalCountOrder = 1,
                        TodayYearupload = todayear
                    };

                    await SqlDataAccess.UpdateInsertQuery(insertquery, insertObj);
                }
                else
                {
                    
                    int getotal = CurrentCount + 1;
                    //Debug.WriteLine($"Date Upload : {DateUpload}  -  UPdates ");
                    //Debug.WriteLine($"Updated Count  : {getotal}");
                    var insertquery = "UPDATE M1_RequestSales_table SET TotalCountOrder = @TotalCountOrder " +
                                    "WHERE SalesRequest = @SalesRequest " +
                                    "AND MonthUpload = @MonthUpload";
                    var insertObj = new
                    {
                        SalesRequest = Convert.ToInt32(dateParts[0]),
                        TotalCountOrder = getotal,
                        MonthUpload = Convert.ToInt32(UploadParts[0])
                    };

                    await SqlDataAccess.UpdateInsertQuery(insertquery, insertObj);
                }


                var strorderTable = "INSERT INTO M1_RequestDetails_table(Branch, SDP_Shoporder, SDP_Sales_Number, " +
                                  "DateUpload, DateSales, DateYear, SDP_Sales_Partnum, Sales_Request_Date, PC_Proposed_Date) " +
                                  "VALUES(@Branch, @SDP_Shoporder, @SDP_Sales_Number, " +
                                  "@DateUpload, @DateSales, @DateYear, @SDP_Sales_Partnum, @Sales_Request_Date, @PC_Proposed_Date)";
                
                var orderTableparam = new
                {
                    Branch = item.Branches,
                    SDP_Shoporder = item.SDP_Shoporder,
                    SDP_Sales_Partnum = item.SDP_Sales_Partnum,
                    SDP_Sales_Number = item.SDP_Sales_Number,
                    Sales_Request_Date = salesDate,
                    PC_Proposed_Date = item.PC_Proposed_Date,
                    DateUpload = Convert.ToInt32(UploadParts[0]),
                    DateSales = Convert.ToInt32(dateParts[0]),
                    DateYear = Fullyear
                };

                await SqlDataAccess.UpdateInsertQuery(strorderTable, orderTableparam);
            }


            
            await Task.Delay(500);
        }

        public async Task UpdateSalesRequestSummaryV2(List<M1ExcelData> dt)
        {
            //var groupedSalesData = dt.GroupBy(o => o.SDPOrderNo)
            //    .Select(g => new
            //    {
            //        Sales_Request_Date = g.First().SalesRequestedShipDate,
            //        DOItoM1 = g.First().DOItoM1,
            //        SDP_Shoporder = g.First().SDPOrderNo,
            //        Branches = g.First().Branch,
            //        DateImport = g.First().DateUpload,
            //        SDP_Sales_Partnum = g.First().SDPSalesPartNo,
            //        SDP_Sales_Number = g.First().SDPSalesQty,
            //        PC_Proposed_Date = g.First().PC1ProposedShipDate
            //    }).Distinct().ToList();

            //foreach (var item in groupedSalesData)
            //{
            //    var formats = new[] { "M/d/yy", "MM/dd/yy" };
            //    if (DateTime.TryParseExact(item.Sales_Request_Date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            //    {
            //        var formattedDate = parsedDate.ToString("MM/dd/yy");
            //        var dateParts = formattedDate.Split('/');

            //        var strDateUpload = item.DateImport;
            //        var UploadParts = strDateUpload.Split('/');
            //        var todaymonth = UploadParts[1];
            //        var todayear = UploadParts[2];

            //        var strquery = "SELECT COALESCE(TotalCountOrder, 0) as TotalCountOrder " +
            //                      "FROM M1_RequestSales_table " +
            //                      "WHERE SalesRequest = @SalesRequest " +
            //                      "AND MonthUpload = @MonthUpload " +
            //                      "AND DateYearupload = @DateYearupload";

            //        var paramsObj = new
            //        {
            //            SalesRequest = Convert.ToInt32(dateParts[0]),
            //            MonthUpload = Convert.ToInt32(todaymonth),
            //            DateYearupload = dateParts[2]
            //        };

            //        int intCount = await SqlDataAccess.GetCountData(strquery, paramsObj);

            //        if (intCount == 0)
            //        {
            //            var insertquery = "INSERT INTO M1_RequestSales_table(MonthUpload, SalesRequest, TotalCountOrder, DateYearupload, TodayYearupload) " +
            //                             "VALUES(@MonthUpload, @SalesRequest, @TotalCountOrder, @DateYearupload, @TodayYearupload)";
            //            var insertObj = new
            //            {
            //                SalesRequest = Convert.ToInt32(dateParts[0]),
            //                MonthUpload = Convert.ToInt32(todaymonth),
            //                DateYearupload = dateParts[2],
            //                TotalCountOrder = 1,
            //                TodayYearupload = todayear
            //            };

            //            await SqlDataAccess.UpdateInsertQuery(insertquery, insertObj);
            //        }
            //        else
            //        {
            //            var getotal = intCount + 1;
            //            var insertquery = "UPDATE M1_RequestSales_table SET TotalCountOrder = @TotalCountOrder " +
            //                             "WHERE SalesRequest = @SalesRequest " +
            //                             "AND MonthUpload = @MonthUpload " +
            //                             "AND DateYearupload = @DateYearupload";
            //            var insertObj = new
            //            {
            //                SalesRequest = Convert.ToInt32(dateParts[0]),
            //                MonthUpload = Convert.ToInt32(todaymonth),
            //                DateYearupload = dateParts[2],
            //                TotalCountOrder = getotal
            //            };

            //            await SqlDataAccess.UpdateInsertQuery(insertquery, insertObj);
            //        }

            //        var strorderTable = "INSERT INTO M1_RequestDetails_table(Branch, SDP_Shoporder, " +
            //                          "SDP_Sales_Partnum, SDP_Sales_Number, Sales_Request_Date, PC_Proposed_Date, DateUpload, DateSales, DateYear) " +
            //                          "VALUES(@Branch, @SDP_Shoporders, @SDP_Sales_Partnums, " +
            //                          "@SDP_Sales_Numbers, @Sales_Request_Date, @PC_Proposed_Date, @DateUpload, @DateSales, @DateYear)";
            //        var orderTableparam = new
            //        {
            //            Branch = item.Branches,
            //            SDP_Shoporders = item.SDP_Shoporder,
            //            SDP_Sales_Partnums = item.SDP_Sales_Partnum,
            //            SDP_Sales_Numbers = item.SDP_Sales_Number,
            //            Sales_Request_Date = item.Sales_Request_Date,
            //            PC_Proposed_Date = item.PC_Proposed_Date,
            //            DateUpload = Convert.ToInt32(todaymonth),
            //            DateSales = Convert.ToInt32(dateParts[0]),
            //            DateYear = dateParts[2]
            //        };

            //        await SqlDataAccess.UpdateInsertQuery(strorderTable, orderTableparam);
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Invalid date format: " + item.Sales_Request_Date);
            //    }
            //}
            await Task.Delay(500);
        }

        public async Task UpdateMonthlySales()
        {
            try
            { 
                var monthdata = await GetMonthSalesTable();
                foreach (var item in monthdata)
                {
                    var strquery = "SELECT RecordID FROM M1_Monthly_Table WHERE DateMonth = @DateMonth AND EndYear = @EndYear";
                    var parameter = new { DateMonth = GlobalUtilities.MonthString(item.MonthUpload), EndYear = item.DateYearupload };

                    var check = await SqlDataAccess.Checkdata(strquery, parameter);
                    if (check)
                    {
                        var strupdate = "UPDATE M1_Monthly_Table SET CurrentRemains = @CurrentRemains " +
                                       "WHERE DateMonth = @DateMonth AND EndYear = @EndYear";
                        var strupdateparams = new
                        {
                            DateMonth = GlobalUtilities.MonthString(item.MonthUpload),
                            EndYear = item.DateYearupload,
                            CurrentRemains = item.TotalOrderCount
                        };
                        await SqlDataAccess.UpdateInsertQuery(strupdate, strupdateparams);
                    }
                    else
                    {
                        var strupdate = "INSERT INTO M1_Monthly_Table(DateMonth, CurrentRemains, EndYear) " +
                                       "VALUES(@DateMonth, @CurrentRemains, @EndYear)";
                        var strupdateparams = new
                        {
                            DateMonth = GlobalUtilities.MonthString(item.MonthUpload),
                            CurrentRemains = item.TotalOrderCount,
                            EndYear = item.DateYearupload
                        };
                        await SqlDataAccess.UpdateInsertQuery(strupdate, strupdateparams);
                    }
                }

                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public async Task UploadAdditionalSummary(List<M1ExcelData> dt)
        {
            try
            {
                await Task.Delay(500);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error : " + ex.Message);
            }
        }

        public async Task ResetsUploadData()
        {
            Debug.WriteLine("Resetting Start ....");

            var strDeleteLackTable = "DELETE FROM M1_Lacking_Summary";
            var strDeleteLackSummary = "DELETE FROM M1_Lacking_table";
            var strDeleteRequestTable = "DELETE FROM M1_RequestSales_table";
            var strDeleteRequestSummary = "DELETE FROM M1_RequestDetails_table";
            var strDeleteDailyTable = "DELETE FROM M1_DailyOrder_Table";
            var strDeleteDailySummary = "DELETE FROM M1_DailyOrder_Summary";
            var strDeleteMainTable = "DELETE FROM M1_Main_table";

            await Task.WhenAll(
                SqlDataAccess.UpdateInsertQuery(strDeleteLackTable, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteLackSummary, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteRequestTable, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteDailyTable, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteDailySummary, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteMainTable, null),
                SqlDataAccess.UpdateInsertQuery(strDeleteRequestSummary, null)
            );
        }

        public async Task<List<DateModel>> GetsDatelist()
        {
            var query = "SELECT FORMAT(MIN(DateUpload), 'yyyy-MM-dd') as FirstDate, " +
                       "FORMAT(MAX(DateUpload), 'yyyy-MM-dd') as LastDate " +
                       "FROM M1_Main_table";

            return await SqlDataAccess.GetData<DateModel>(query);
        }

        private DateTime SafeDate(object value)
        {
            if (value is DateTime)
            {
                return (DateTime)value;
            }
            else if (value != null && DateTime.TryParse(value.ToString(), out var result))
            {
                return result;
            }
            return DateTime.MinValue;
        }

        public async Task<List<MonthlySales>> GetMonthSalesTable()
        {
            //var strquery = "SELECT COUNT(*) as TotalCountOrder, DateUpload, DateYear " +
            //              "FROM M1_RequestDetails_table GROUP BY DateUpload, DateYear";
            var strquery = "SELECT MonthUpload,SUM(TotalCountOrder) AS TotalOrderCount, DateYearupload " +
                           "FROM  M1_RequestSales_table " +
                           "GROUP BY MonthUpload, DateYearupload " +
                           "ORDER BY MonthUpload";


            return await SqlDataAccess.GetData<MonthlySales>(strquery);
        }


        // DISPLAY THE SUMMARY SALES RESULT
        public async Task<DataTable> SalesResultSummary()
        {
            List<SalesMonthInfo> monthYearList = new List<SalesMonthInfo>();

            DataTable dates = await GetSalesMonths(DateTime.Now.ToString("yyyy"));

            // Fill the list with SalesRequest and Year
            foreach (DataRow row in dates.Rows)
            {
                monthYearList.Add(new SalesMonthInfo
                {
                    MonthAbbreviation = row["SalesRequest"].ToString(),
                    Year = row["DateYearupload"].ToString()
                });
            }

            // Remove duplicates and order by year
            //monthYearList = monthYearList
            //    .GroupBy(m => m.MonthAbbreviation + m.Year)
            //    .Select(g => g.First())
            //    .OrderBy(m => Convert.ToInt32(m.Year))
            //    .ToList();

            // Build dynamic column strings
            string[] formattedColumns = monthYearList
                .Select(m => $"CONCAT(COALESCE([{m.MonthAbbreviation}], 0), ':', MonthUpload) AS '{m.MonthAbbreviation} / {m.Year}'")
                .ToArray();

            string[] formattedOrders = monthYearList
                .Select(m => $"[{m.MonthAbbreviation}]")
                .Distinct()
                .ToArray();

            string joinedColumns = string.Join(", ", formattedColumns);
            string joinOrders = string.Join(", ", formattedOrders);

            string strquery = $@"
                    SELECT 
                        MonthUpload, {joinedColumns}  
                    FROM 
                        (SELECT 
                            MonthUpload, 
                            SalesRequest AS SalesMonth, 
                            TotalCountOrder
                         FROM 
                            M1_RequestSales_table) AS SourceTable
                    PIVOT 
                        (SUM(TotalCountOrder) 
                         FOR SalesMonth IN ({joinOrders})) AS PivotTable;";

            //Debug.WriteLine(strquery);

            DataTable result = await SqlDataAccess.GetDataByDataTable(strquery);
            return result;
        }

    }
}