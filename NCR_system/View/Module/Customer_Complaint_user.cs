using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.View.AddForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Module
{
    public partial class Customer_Complaint_user : UserControl
    {
        private readonly ICustomerComplaint _cust;


        public List<CustomerModel> cuslist { get; private set; } = new List<CustomerModel>();
        public DataGridView Customgrid { get { return CustomDatagrid; } }



        public Customer_Complaint_user(ICustomerComplaint cust)
        {
            InitializeComponent();
            _cust = cust;
        }


        public async Task DisplayCustomer(int proc)
        {
            try
            {
                // For Displaying Customer
                CustomDatagrid.DataSource = null;
                var getdata = (await _cust.GetCustomerData(proc)).ToList();
                cuslist = getdata;

             
                CustomDatagrid.DataSource = cuslist;
               


                if (proc == 0)
                {
                    CustomDatagrid.Columns["RecordID"].Visible = false;
                    CustomDatagrid.Columns["CustomerName"].Visible = true;
                    CustomDatagrid.Columns["CCtype"].Visible = false;
                    CustomDatagrid.Columns["Status"].DisplayIndex = 8;
                    CustomDatagrid.Columns["Action"].DisplayIndex = 9;
                }
                else
                {
                    CustomDatagrid.Columns["RecordID"].Visible = false;
                    CustomDatagrid.Columns["RegNo"].Visible = false;
                    CustomDatagrid.Columns["CustomerName"].Visible = false;
                    CustomDatagrid.Columns["CCtype"].Visible = false;        
                }



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
                var openCounts = cuslist
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
                CustSummaryGrid.DataSource = summary;


            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customer data: {ex.Message}");
            }
        }

        private void CustomDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if(e.Value == null)
                return;

            if(CustomDatagrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                switch(e.Value.ToString())
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
            else if (CustomDatagrid.Columns[e.ColumnIndex].Name == "Status")
            {
                string checkstats = e.Value.ToString() == "1" ? "Open" : "Close";
                e.Value = checkstats;

                if (checkstats == "Open")
                {
                    e.CellStyle.ForeColor = Color.Green;
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Red;
                }


                e.FormattingApplied = true;
            }
        }

        private void OpenCC_Click(object sender, EventArgs e)
        {
            var add = new AddCustomerComplaint(_cust);
            add.ShowDialog();
        }

        private void Customer_Complaint_user_Load(object sender, EventArgs e)
        {
            SelectedProcess.SelectedIndex = 0;

        }

        private async void SelectedProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
           await DisplayCustomer(SelectedProcess.SelectedIndex);
        }

        private void Externalbtn_Click(object sender, EventArgs e)
        {
            var add = new Form1(_cust);
            add.ShowDialog();
        }
    }
}
