using NCR_system.Interface;
using NCR_system.Models;
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


        public async Task DisplayCustomer()
        {
            try
            {
                // For Displaying Customer
                CustomDatagrid.DataSource = null;
                cuslist = (await _cust.GetCustomerData(0)).ToList();
                CustomDatagrid.DataSource = cuslist;



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
                    .Where(c => c.Status == "True")
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
                switch (e.Value.ToString())
                {
                    case "True":
                        e.Value = "Open";
                        break;
                    case "False":
                        e.Value = "Close / Completed";
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
