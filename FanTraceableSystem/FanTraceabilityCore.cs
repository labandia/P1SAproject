using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public static class FanTraceabilityCore
    {
        public static readonly Dictionary<int, string> SectionMap = new Dictionary<int, string>()
        {
            {1, "Molding Section"},
            {2, "Press Section"},
            {3, "Rotor Section"},
            {4, "Winding Section"},
            {5, "Circuit Section"},
            {6, "Oilproof Section"},
            {7, "Harness Section"}
        };

        public static int GetShift()
        {
            TimeSpan time = DateTime.Now.TimeOfDay;

            TimeSpan dayStart = new TimeSpan(6, 30, 0);    // 6:30 AM
            TimeSpan nightStart = new TimeSpan(18, 30, 0); // 6:30 PM

            // Day shift: 6:30 AM to before 6:30 PM
            if (time >= dayStart && time < nightStart)
            {
                return 0; // Day shift
            }

            // Night shift
            return 1; // Night shift
        }


        public static (int start, int end, bool hasNext) CalculatePage(int pageNumber, int pageSize, int returnedCount, int totalCount)
        {
            int start = ((pageNumber - 1) * pageSize) + 1;
            int end = start + returnedCount - 1;

            if (totalCount == 0)
            {
                start = 0;
                end = 0;
            }

            bool hasNext = end < totalCount;

            return (start, end, hasNext);
        }


        public static void ConfigureColumn(DataGridView grid, string name, int width)
        {
            if (!grid.Columns.Contains(name)) return;

            var col = grid.Columns[name];

            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
        }


        public static string FormatShift(string shift)
        {
            if (string.IsNullOrEmpty(shift)) return "N/A";
            return shift == "0" ? "DAYSHIFT" : "NIGHTSHIFT";
        }

    }
}
