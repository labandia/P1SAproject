using NCR_system.Interface;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
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
    public partial class ShipRejected : UserControl
    {
        private readonly IShipRejected _ship;

        public DataGridView Customgrid { get { return RejectedGrid; } }

        public ShipRejected(IShipRejected ship)
        {
            InitializeComponent();
            _ship = ship;
        }

        public async Task DisplayRejected(int proc)
        {
            try
            {
                // For Displaying Customer
                var ShipList = (await _ship.GetRejectedShipData(proc)).ToList();
                RejectedGrid.DataSource = ShipList;


                RejectedGrid.Columns["RegNo"].DisplayIndex = 0;
                RejectedGrid.Columns["DateIssued"].DisplayIndex = 1;
                RejectedGrid.Columns["IssueGroup"].DisplayIndex = 2;
                RejectedGrid.Columns["SectionID"].DisplayIndex = 3;
                RejectedGrid.Columns["ModelNo"].DisplayIndex = 4;
                RejectedGrid.Columns["Quantity"].DisplayIndex = 5;
                RejectedGrid.Columns["Contents"].DisplayIndex = 6;
                RejectedGrid.Columns["DateCloseReg"].DisplayIndex = 7;
                RejectedGrid.Columns["Status"].DisplayIndex = 8;
                RejectedGrid.Columns["Edit"].DisplayIndex = 9;
                RejectedGrid.Columns["Delete"].DisplayIndex = 10;



                // 🔹 Define all known sections
                var sections = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Molding"),
                    new KeyValuePair<int, string>(2, "Press"),
                    new KeyValuePair<int, string>(3, "Rotor"),
                    new KeyValuePair<int, string>(4, "Winding"),
                    new KeyValuePair<int, string>(5, "Circuit")
                };

                var statusToCount = new[] { 1, 2, 3 };
                // 🔹 Group existing open items
                var openCounts = ShipList
                     .Where(c => statusToCount.Contains(c.Status))
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

        private void OpenShip_Click(object sender, EventArgs e)
        {
            var rej = new Rejected(_ship);

            var openmodel = new AddShipment(_ship, 1,  this, rej);
            openmodel.ShowDialog();
        }

        private void RejectedGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Make sure user clicked on a valid row (not header)
            if (e.RowIndex < 0)
                return;


            // Get the clicked column
            var column = RejectedGrid.Columns[e.ColumnIndex];

            // Get the clicked row
            var row = RejectedGrid.Rows[e.RowIndex];

            var recordID = row.Cells["RecordID"].Value;
            var type = row.Cells["Process"].Value;

            if (column.Name == "Edit")
            {
                int processtype = Convert.ToInt32(type);
                // You can get the row data like this:
                var openedit = new EditShipments(_ship, this, Convert.ToInt32(recordID), processtype);
                openedit.ShowDialog();

            }
            else if (column.Name == "Delete")
            {
                // Handle Delete image click
                DialogResult result = MessageBox.Show("Are you sure you want to delete?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    // Remove the row or perform deletion
                    MessageBox.Show($"Delete clicked on row {e.RowIndex} - Record ID selected:  {recordID}");
                }
            }
        }
    }
}
