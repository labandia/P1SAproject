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
    public partial class Inprocess_control : UserControl
    {
        private readonly IInprocess _inp;
        private readonly ISummary _sum;


        public DataGridView InprocessgridV2 { get { return InprocessGrid; } }

        public Inprocess_control(IInprocess inp, ISummary sum)
        {
            InitializeComponent();
            _inp = inp;
            _sum = sum;
        }


        public async Task DisplayRejected()
        {
            try
            {
                // For Displaying Customer
                var inprocesslist = (await _inp.GetInprocessData(1)).ToList();
                InprocessGrid.DataSource = inprocesslist;


                // 🔹 Define all known sections
                var sections = new List<KeyValuePair<int, string>>()
                {
                    new KeyValuePair<int, string>(1, "Molding"),
                    new KeyValuePair<int, string>(2, "Press"),
                    new KeyValuePair<int, string>(3, "Rotor"),
                    new KeyValuePair<int, string>(4, "Winding"),
                    new KeyValuePair<int, string>(5, "Circuit")
                };

                
                InprocessGrid.Columns["DateEncounter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                InprocessGrid.Columns["DateEncounter"].Width = 150;

                InprocessGrid.Columns["ProcEncounter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                InprocessGrid.Columns["ProcEncounter"].Width = 150;

                InprocessGrid.Columns["TitleEmail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                InprocessGrid.Columns["Invest"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                InprocessGrid.Columns["cause"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                InprocessGrid.Columns["Model"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                InprocessGrid.Columns["Defect"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                InprocessGrid.Columns["P1saStatus"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InprocessGrid.Columns["Shift"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InprocessGrid.Columns["cause"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InprocessGrid.Columns["Line"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InprocessGrid.Columns["NGQty"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                InprocessGrid.Columns["SectionDep"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


                InprocessGrid.Columns["ShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                InprocessGrid.Columns["ShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                InprocessGrid.Columns["ShopOrder"].Width = 150;

                var countItems = await _inp.GetCustomersOpenItem(1);


                foreach (var items in countItems)
                {
                    switch (items.DepartmentName)
                    {
                        case "Molding":
                            MoldText.Text = items.totalOpen.ToString();
                            break;
                        case "Press":
                            PressText.Text = items.totalOpen.ToString();
                            break;
                        case "Rotor":
                            RotorText.Text = items.totalOpen.ToString();
                            break;
                        case "Winding":
                            WindText.Text = items.totalOpen.ToString();
                            break;
                        default:
                            CircuitText.Text = items.totalOpen.ToString();
                            break;
                    }

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Inprocess_control_Load(object sender, EventArgs e)
        {

        }

        private void InprocessGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (InprocessGrid.Columns[e.ColumnIndex].Name == "Status")
            {
                int checkstatus = (int)e.Value;


                e.Value = (checkstatus == 0) ? "Close" : "Open";   
                e.FormattingApplied = true;

                if (checkstatus == 1)
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(78, 166, 101);
                }
                else
                {
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(184, 94, 104);
                }
            }
            else if (InprocessGrid.Columns[e.ColumnIndex].Name == "Shift")
            {
                int checkshift = (int)e.Value;

                e.Value = (checkshift == 0) ? "DS" : "NS";
            }
        }
    }
}
