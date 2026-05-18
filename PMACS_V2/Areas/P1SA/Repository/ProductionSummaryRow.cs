using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMACS_V2.Areas.P1SA.Repository
{
    public class ProductionSummaryRow
    {
        public string Category { get; set; }

        /// <summary>
        /// Key: column label from SQL e.g. "May '26", "Jun '26"
        /// Value: the numeric value for that month
        /// </summary>
        /// 
        public Dictionary<string, double> MonthValues { get; set; } = new Dictionary<string, double>();


        // Indexer for convenient access: row["May '26"]
        public double this[string monthLabel]
        {
            get => MonthValues.TryGetValue(monthLabel, out var val) ? val : 0;
            set => MonthValues[monthLabel] = value;
        }

        // Optional: get value with a default if column doesn't exist
        public double GetValue(string monthLabel, double defaultValue = 0)
            => MonthValues.TryGetValue(monthLabel, out var val) ? val : defaultValue;
    }
}