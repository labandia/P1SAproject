using NCR_system.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Module
{
    public partial class Rejected : UserControl
    {
        private readonly IShipRejected _ship;

        public DataGridView Customgrid { get { return RejectedGrid; } }

        public Rejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
        }

        public async Task DisplayRejected()
        {
            try
            {
                // For Displaying Customer
                RejectedGrid.DataSource = null;
                var rejectlist = (await _ship.GetRejectedShipData(0)).ToList();
                RejectedGrid.DataSource = rejectlist;


                // 🔹 Define all known sections
                var sections = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Molding"),
                    new KeyValuePair<int, string>(2, "Press"),
                    new KeyValuePair<int, string>(3, "Rotor"),
                    new KeyValuePair<int, string>(4, "Winding"),
                    new KeyValuePair<int, string>(5, "Circuit")
                };

               
                // 🔹 Group existing open items
                var openCounts = rejectlist
                     .Where(c => c.Status == 1)
                    .GroupBy(c => c.SectionID)
                    .ToDictionary(g => g.Key, g => g.Count());

                // 🔹 Merge all sections with counts (include 0 if missing)
                var summary = sections
                    .Select(s => new
                    {
                        Section = s.Value,
                        TotalOpen = openCounts.ContainsKey(s.Key) ? openCounts[s.Key] : 0
                    })
                    .ToList();

                // 🔹 Display summary
                SummaryRejected.DataSource = summary;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RejectedGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (RejectedGrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "P1SA MOLDING";
                        e.FormattingApplied = true;
                        break;
                    case "2":
                        e.Value = "P1SA PRESS";
                        e.FormattingApplied = true;
                        break;
                    case "3":
                        e.Value = "P1SA ROTOR";
                        e.FormattingApplied = true;
                        break;
                    case "4":
                        e.Value = "P1SA WINDING";
                        e.FormattingApplied = true;
                        break;
                    default:
                        e.Value = "P1SA CIRCUIT";
                        e.FormattingApplied = true;
                        break;
                }
            }
            else if (RejectedGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "Open";
                        break;
                    case "0":
                        e.Value = "Close / Completed";
                        break;
                    case "2":
                        e.Value = "Report Ok";
                        break;
                    case "3":
                        e.Value = "For Circulation";
                        break;
                    default:
                        e.Value = "Unknown Status";
                        break;
                }

                e.FormattingApplied = true;
            }
        }
    }
}
