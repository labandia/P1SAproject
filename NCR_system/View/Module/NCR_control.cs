using NCR_system.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;

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

                //RecordID
                NCRGrid.Columns["Category"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Category"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                NCRGrid.Columns["Category"].DisplayIndex = 0;
                //NCRGrid.Columns["Category"].Visible = false;



                NCRGrid.Columns["RegNo"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["RegNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["RegNo"].Width = 150;
                NCRGrid.Columns["RegNo"].DisplayIndex = 1;
                //NCRGrid.Columns["RegNo"].Visible = false;

                NCRGrid.Columns["DateIssued"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["DateIssued"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["DateIssued"].Width = 150;
                NCRGrid.Columns["DateIssued"].DisplayIndex = 2;

                NCRGrid.Columns["IssuedGroup"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["IssuedGroup"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["IssuedGroup"].Width = 180;
                NCRGrid.Columns["IssuedGroup"].DisplayIndex = 3;

                NCRGrid.Columns["SectionID"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["SectionID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["SectionID"].Width = 180;
                NCRGrid.Columns["SectionID"].DisplayIndex = 4;

                NCRGrid.Columns["ModelNo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                NCRGrid.Columns["ModelNo"].DisplayIndex = 5;

                NCRGrid.Columns["Quantity"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["Quantity"].Width = 100;
                NCRGrid.Columns["Quantity"].DisplayIndex = 6;

                NCRGrid.Columns["Contents"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Contents"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["Contents"].Width = 100;
                NCRGrid.Columns["Contents"].DisplayIndex = 7;
      
                NCRGrid.Columns["DateRegist"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["DateRegist"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["DateRegist"].Width = 120;
                NCRGrid.Columns["DateRegist"].DisplayIndex = 8;

                NCRGrid.Columns["Status"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["Status"].Width = 150;
                NCRGrid.Columns["Status"].DisplayIndex = 9;

                // Target Date
                NCRGrid.Columns["TargetDate"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["TargetDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                NCRGrid.Columns["TargetDate"].DisplayIndex = 10;


                NCRGrid.Columns["FilePath"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                NCRGrid.Columns["FilePath"].DisplayIndex = 11;

                // Date Close
                NCRGrid.Columns["DateCloseReg"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["DateCloseReg"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["DateCloseReg"].Width = 150;
                NCRGrid.Columns["DateCloseReg"].DisplayIndex = 12;

                // Circulation 
                NCRGrid.Columns["CircularStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["CircularStatus"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["CircularStatus"].Width = 150;
                NCRGrid.Columns["CircularStatus"].DisplayIndex = 13;


                NCRGrid.Columns["Edit"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Edit"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["Edit"].Width = 150;
                NCRGrid.Columns["Edit"].DisplayIndex = 14;


                NCRGrid.Columns["Delete"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                NCRGrid.Columns["Delete"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                NCRGrid.Columns["Delete"].Width = 150;
                NCRGrid.Columns["Delete"].DisplayIndex = 15;

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

        private void OpenReject_Click(object sender, EventArgs e)
        {

        }
    }
}
