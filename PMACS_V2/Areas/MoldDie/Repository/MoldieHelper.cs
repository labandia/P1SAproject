using PMACS_V2.Areas.P1SA.Models;
using PMACS_V2.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PMACS_V2.Areas.MoldDie.Repository
{
    public static class MoldieHelper
    {
        // ===========================================================
        // MOLD DIE PART NUMBER LOGIC
        // ===========================================================
        public static async Task<bool> MoldiePartNoLogic(
            DieMoldMonitoringModel mold, string dateOnly)
        {
            // ===========================================================
            // CASE 1: Part number already exists → compute new total
            // ===========================================================

            bool partExists = await SqlDataAccess.Checkdata(
                       "SELECT 1 FROM DieMold_Daily WHERE PartNo = @PartNo",
                       new { PartNo = mold.PartNo });

            // Get last total shot
            int lasttotal = partExists ? await SqlDataAccess.GetCountData($@"
                    SELECT TOP 1 Total
                    FROM DieMold_Daily
                    WHERE PartNo = @PartNo 
                    ORDER BY RecordID DESC",
                new { PartNo = mold.PartNo }) : 0;

            Debug.WriteLine("Done Getting the Last Cycle shot  ... ");
            Debug.WriteLine(mold.PartNo + " --- " + mold.ProcessID);

            var history = await GetMasterlistParts(mold.PartNo);

            //if (history == null) return false;
            Debug.WriteLine("Done Getting the Last Record History  ... ");
            Debug.WriteLine(history.Dimension_Quality);

            string serial = mold.DieSerial;

            var (moldtotal, moldstatus) = CalculateTotalAndStatus(
                  lasttotal,
                  mold.CycleShot,
                  mold.ProcessID,
                  history.Dimension_Quality);
            Debug.WriteLine("Done Calculating Total and Status ... ");

            // =====================================================
            // DAILY MONITORING 
            // =====================================================
            await UpsertDailyAsync(mold.PartNo, dateOnly, mold.CycleShot, moldtotal, moldstatus, mold);

            Debug.WriteLine("Done Updating Daily ... ");

            // =====================================================
            // INSERT MONITOR LOG
            // =====================================================

            await UpsertMonitor(mold.PartNo, dateOnly, mold.CycleShot);

            Debug.WriteLine("Done Updating Monitoring ... ");

            // =====================================================
            // 🔥 UPDATE OTHER PART NUMBERS WITH SAME DIE SERIAL
            // =====================================================

            var getpartnumbers = await GetSamePartsNo(mold.DieSerial, mold.ProcessID, mold.PartNo);

            Debug.WriteLine("Done Retriving  Partnumbers ... ");

            foreach (var part in getpartnumbers)
            {
                await UpsertDailyAsync(
                    part,
                    dateOnly,
                    mold.CycleShot,
                    moldtotal,
                    moldstatus,
                    mold);
            }

            await UpdateInputChecker("PartNo", mold.PartNo, dateOnly);

            Debug.WriteLine("Done Proess Completed !!!!!!!!! ... ");
            return true;

        }


        // GET MOLDIE PARTS MASTERLIST 

        public static async Task<DieMoldMonitoringModel> GetMasterlistParts(string partno)
        {
            bool isPartNo = !string.IsNullOrWhiteSpace(partno)
                          && partno.StartsWith("0");


            string sql = isPartNo
                ? @"SELECT 
                      PartNo,PartDescription
                     ,Dimension_Quality,DieSerial
                     ,DieNumber
                FROM DieMold_MoldingMainParts
                WHERE PartNo = @searchValue"
                : @"SELECT 
                      PartNo,PartDescription
                     ,Dimension_Quality,DieSerial
                     ,DieNumber
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @searchValue";

            var items = await SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql, new { SearchValue = partno?.Trim() });

            return items.FirstOrDefault();
        }


        public static (int Total, int Status) CalculateTotalAndStatus(
          int lastTotal,
          int cycleShot,
          string processId,
          string dimensionQuality)
        {

            int newTotal = lastTotal + cycleShot;
            int status = 0;

            dimensionQuality = dimensionQuality ?? "";

            // Highest priority rules first
            if (dimensionQuality.Contains("R60-25") && processId == "M002")
            {
                if (newTotal >= 38000 && newTotal <= 39999) status = 1;
                else if (newTotal > 40000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (dimensionQuality.Contains("CRV40-56"))
            {
                if (newTotal >= 28000 && newTotal <= 29999) status = 1;
                else if (newTotal > 30000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M003")
            {
                if (newTotal >= 68000 && newTotal <= 69999) status = 1;
                else if (newTotal > 70000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M005")
            {
                if (newTotal >= 28000 && newTotal <= 29999) status = 1;
                else if (newTotal > 30000) { status = 2; newTotal = 0; }
                return (newTotal, status);
            }

            if (processId == "M002" || processId == "M003")
            {
                if (newTotal >= 48000 && newTotal <= 49999) status = 1;
                else if (newTotal > 50000) { status = 2; newTotal = 0; }
            }

            return (newTotal, status);
        }


        public static async Task UpsertDailyAsync(string partNo,
           string date,
           int cycleShot,
           int total,
           int status, DieMoldMonitoringModel mold)
        {
            bool checkCycle = cycleShot == 0;

            int TotalCycle = checkCycle ? 0 : total;
            int NewStats = checkCycle ? 2 : status;



            string sql = @"
                    INSERT INTO DieMold_Daily
                    (PartNo, DateInput, CycleShot, Total, MachineNo, Remarks, Mincharge, Status)
                    VALUES
                    (@PartNo, @DateInput, @CycleShot, @Total, @MachineNo, @Remarks, @Mincharge, @Status);";

            await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                PartNo = partNo,
                DateInput = date,
                CycleShot = cycleShot,
                Total = TotalCycle,
                MachineNo = mold.MachineNo,
                Remarks = mold.Remarks,
                Mincharge = mold.Mincharge,
                Status = NewStats
            });
        }

        public static async Task UpsertMonitor(string partNo, string date, int cycleShot)
        {
            string sql = @"
                UPDATE DieMoldMonitor
                SET TotalDie = @TotalDie
                WHERE PartNo = @PartNo
                  AND CAST(DateAction AS DATE) = CAST(@DateAction AS DATE);

                IF @@ROWCOUNT = 0
                BEGIN
                    INSERT INTO DieMoldMonitor (PartNo, DateAction, TotalDie)
                    VALUES (@PartNo, @DateAction, @TotalDie);
                END";

            await SqlDataAccess.UpdateInsertQuery(sql, new
            {
                PartNo = partNo,
                DateAction = date,
                TotalDie = cycleShot
            });
        }




        public static async Task<List<string>> GetSamePartsNo(string dieSerial, string processId, string currentPart)
        {
            return await SqlDataAccess.GetlistStrings(@"
                SELECT DISTINCT PartNo
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial
                  AND ProcessID = @ProcessID
                  AND PartNo <> @PartNo",
                new
                {
                    DieSerial = dieSerial,
                    ProcessID = processId,
                    PartNo = currentPart
                });
        }

        // ===========================================================
        // MOLD DIE SERIAL NUMBER LOGIC
        // ===========================================================

        public static async Task<bool> MoldieDieSerialLogic(DieMoldMonitoringModel mold, string dateOnly)
        {
            // Get all related PartNo using DieSerial
            var partList = await GetPartNoByDieSerial(mold.DieSerial);
            if (!partList.Any()) return false;


            foreach (var part in partList)
            {
                // Get last total BEFORE this date
                string getLastTotal = @"  
                            SELECT TOP 1 ISNULL(Total, 0)
                            FROM DieMold_Daily d
                            INNER JOIN DieMold_MoldingMainParts p 
                            ON d.PartNo = p.PartNo
                            WHERE p.DieSerial = @DieSerial
                              AND d.DateInput < @DateInput
                            ORDER BY DateInput DESC, RecordID DESC";

                int lastTotal = await SqlDataAccess.GetCountData(getLastTotal, new
                {
                    DieSerial = mold.DieSerial,
                    DateInput = mold.DateInput
                });

                Debug.WriteLine("Last Cycle Time Die Serial : " + lastTotal);

                var history = await GetMasterlistParts(part);

 

                //if (history == null) return false;
                Debug.WriteLine("Done Getting the Last Record History  ... ");

                if (history == null) return false;

                //if (history == null) return false;
                Debug.WriteLine("Done Getting the Last Record History  ... ");
                Debug.WriteLine(history.Dimension_Quality);

                // correct Dimension_Quality per PartNo
                var (moldtotal, moldstatus) = CalculateTotalAndStatus(
                  lastTotal,
                  mold.CycleShot,
                  mold.ProcessID,
                  history.Dimension_Quality);

                Debug.WriteLine("Done Calculating Total and Status ... ");

                // =====================================================
                // DAILY MONITORING 
                // =====================================================
                await UpsertDailyAsync(part, mold.DateInput, mold.CycleShot, moldtotal, moldstatus, mold);
                Debug.WriteLine("Done Updating Daily ... ");

                // =====================================================
                // INSERT MONITOR LOG
                // =====================================================

                await UpsertMonitor(part, dateOnly, mold.CycleShot);

                Debug.WriteLine("Done Updating Monitoring ... ");


            }

            await UpdateInputChecker("DieSerial", mold.DieSerial, dateOnly);

            Debug.WriteLine("✔ DieSerial update applied to ALL related PartNo");
            return true;

        }



        public static Task<List<string>> GetPartNoByDieSerial(string dieSerial)
        {
            string sql = @"
                SELECT PartNo
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial";

            return SqlDataAccess.GetData<string>(sql, new { DieSerial = dieSerial });
        }

        public static async Task<List<DieMoldMonitoringModel>> GetByDieSerialAsync(string dieSerial)
        {
            const string sql = @"
                SELECT PartNo, PartDescription,
                       Dimension_Quality, DieSerial, DieNumber
                FROM DieMold_MoldingMainParts
                WHERE DieSerial = @DieSerial";

            return (await SqlDataAccess.GetData<DieMoldMonitoringModel>(
                sql, new { DieSerial = dieSerial.Trim() })).ToList();
        }


     


        // ===========================================================
        // COMMON FUNCTION 
        // ===========================================================

        private static async Task UpdateInputChecker(
            string column, 
            string value, 
            string dateinput)
        {
            string sql = $@"
                    IF EXISTS (
                        SELECT 1 FROM DieMoldDailyInputChecker
                        WHERE {column} = @Value AND DateInput = @DateInput
                    )
                    BEGIN
                        UPDATE DieMoldDailyInputChecker
                        SET Count = ISNULL(Count, 0) + 1
                        WHERE {column} = @Value AND DateInput = @DateInput
                    END
                    ELSE
                    BEGIN
                        INSERT INTO DieMoldDailyInputChecker ({column}, DateInput, Count)
                        VALUES (@Value, @DateInput, 1)
                    END";
            await SqlDataAccess.UpdateInsertQuery(sql,
                        new { Value = value, DateInput = dateinput });
        }



        public static async Task<bool> UpdateAllMoldieData(DieMoldMonitoringModel model)
        {
            /* --------------------------------------------------
                * 1. Update FIRST (Selected) Record
                * --------------------------------------------------*/
            await SqlDataAccess.UpdateInsertQuery(@"
                    UPDATE DieMold_Daily
                    SET 
                        DateInput = @DateInput,
                        CycleShot = @CycleShot,
                        Total      = @Total,
                        Status     = @Status
                    WHERE RecordID = @RecordID",
             new
             {
                 model.RecordID,
                 model.DateInput,
                 model.CycleShot,
                 model.Total,
                 model.Status
             });
            // 2. Get all the MoldDie_Daily records and stops if the last record is 0

            /* --------------------------------------------------
              * 2. Get ALL records (LAST → FIRST)
              * --------------------------------------------------*/
            var records = await SqlDataAccess.GetData<DieMoldMonitoringModel>(
                         @"
                        SELECT RecordID, DateInput, CycleShot, Total, Status
                        FROM DieMold_Daily
                        WHERE PartNo = @PartNo
                        ORDER BY DateInput DESC",
                         new { model.PartNo });

            /* --------------------------------------------------
            * 3. Get Only records of the Last CycleShot that is not 0 (LAST → FIRST)
            * --------------------------------------------------*/
            //var validRecords = records.TakeWhile(r => r.CycleShot != 0).ToList();

            //List<DieMoldMonitoringModel> nonZero = records
            //    .Where(r => r.CycleShot != 0)
            //    .ToList();

            List<DieMoldMonitoringModel> Lastrecords = new List<DieMoldMonitoringModel>();

            foreach (var record in records)
            {
                if (record.CycleShot == 0)
                {
                    break;
                }
                Lastrecords.Add(record);
            }

            // SET ASCENDING ORDER


            /* --------------------------------------------------
            * 4. LOOP through the records and compute the new Total = current total + NEXT record CycleShot
            * --------------------------------------------------*/
            //for (int i = 0; i < Lastrecords.Count; i++)
            //{
            //    //if (i == 0) continue;

            //    int newTotal;
            //    // 4. Total = current total + NEXT record CycleShot
            //    //newTotal = Lastrecords[i - 1].Total + Lastrecords[i].CycleShot;
            //    Debug.WriteLine("Index: " + i);
            //    //Lastrecords[i].Total = newTotal;
            //    Debug.WriteLine("Current CycleShot: " + Lastrecords[i].CycleShot);

            //    Debug.WriteLine("CycleShot: " + Lastrecords[i].CycleShot + " | Total: " + Lastrecords[i].Total );
            //}
            int counter = 0;

            for (int i = Lastrecords.Count - 1; i >= 0; i--)
            {

                //Debug.WriteLine("Index: " + i);
                //Debug.WriteLine("Current CycleShot: " + Lastrecords[i].CycleShot);
                //Debug.WriteLine(
                //    "CycleShot: " + Lastrecords[i].CycleShot +
                //    " | Total: " + Lastrecords[i].Total
                //);
                if (counter == 0)
                {
                    counter = 1;
                    continue;
                }

                int newTotal  = Lastrecords[i + 1].Total + Lastrecords[i].CycleShot;

                Debug.WriteLine("CycleShot: " + Lastrecords[i].CycleShot + " | Total: " + Lastrecords[i].Total + " | newTotal : " + newTotal);
                /* --------------------------------------------------
                * 5. UPDATE the newTotalValue to the total of the MoldDie_Daily column
                * --------------------------------------------------*/
                await SqlDataAccess.UpdateInsertQuery(@"
                    UPDATE DieMold_Daily
                    SET Total = @Total
                    WHERE PartNo = @PartNo
                      AND DateInput = @DateInput",
                   new
                   {
                       Total = newTotal,
                       PartNo = model.PartNo,
                       DateInput = Lastrecords[i].DateInput
                   });

            }

            return await Task.FromResult(true);
        }

    }
}