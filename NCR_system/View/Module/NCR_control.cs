using NCR_system.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Linq;

namespace NCR_system.View.Module
{
    public partial class NCR_control : UserControl
    {
        private readonly INCR _ncr;

        public NCR_control(INCR ncr)
        {
            InitializeComponent();
            _ncr = ncr;
        }

        public async Task DisplayNCR(int procs)
        {
            try
            {
                // For Displaying Customer
                //NCRGrid.DataSource = null;
                var inprocesslist = (await _ncr.GetNCRData(procs)).ToList();
                NCRGrid.DataSource = inprocesslist;
                CountDisplay.Text = "Records number : " + inprocesslist.Count.ToString(); 


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
                //var openCounts = inprocesslist
                //     .Where(c => c.Status == 1)
                //    .GroupBy(c => c.SectionID)
                //    .ToDictionary(g => g.Key, g => g.Count());

                //// 🔹 Merge all sections with counts (include 0 if missing)
                //var summary = sections
                //    .Select(s => new
                //    {
                //        Section = s.Value,
                //        TotalOpen = openCounts.ContainsKey(s.Key) ? openCounts[s.Key] : 0
                //    })
                //    .ToList();

                // 🔹 Display summary
                //SummaryInprocess.DataSource = summary;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NCRGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null)
                return;

            if (NCRGrid.Columns[e.ColumnIndex].Name == "SectionID")
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
            else if (NCRGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                switch (e.Value.ToString())
                {
                    case "1":
                        e.Value = "Open";
                        break;
                    case "0":
                        e.Value = "Close / Completed";
                        break;
                    default:
                        e.Value = "Unknown Status";
                        break;
                }

                e.FormattingApplied = true;
            }
        }

        private void NCR_control_Load(object sender, EventArgs e)
        {
            SelectedProcess.SelectedIndex = 0;
        }

        private async void SelectedProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
             await DisplayNCR(SelectedProcess.SelectedIndex);
        }

        private void NCRGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && NCRGrid.Columns[e.ColumnIndex].Name == "FilePath")
            {
                string filePath = NCRGrid.Rows[e.RowIndex].Cells["FilePath"].Value?.ToString();

                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    System.Diagnostics.Process.Start(filePath);
                }
                else
                {
                    MessageBox.Show("File not found:\n" + filePath);
                }
            }
        }
    }
}
