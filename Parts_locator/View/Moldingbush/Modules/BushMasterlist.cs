using Parts_locator.Interface;
using System;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush.Modules
{
    public partial class BushMasterlist : UserControl
    {
        private readonly IRawMats _raw;
        public DataGridView shafttable { get { return ShaftassyGridview; } }

        public BushMasterlist(IRawMats raw)
        {
            InitializeComponent();
            _raw = raw;
        }

        public async void UpdateDisplayTable()
        {
            int controlselect = tabControl1.SelectedIndex + 1;
            var data = await _raw.GetRawMatProductByType(controlselect);
            ShaftassyGridview.DataSource = data;
            ShaftassyGridview.Columns["Edit"].DisplayIndex = controlselect == 1 ? 5 : 4;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) => UpdateDisplayTable();
        private void ShaftassyGridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a header
            if (e.RowIndex < 0)
            {
                return;
            }

            string strpartnum = ShaftassyGridview.Rows[e.RowIndex].Cells["PartNumbershaft"].Value.ToString();
            string intqty = ShaftassyGridview.Rows[e.RowIndex].Cells["ShaftQuantity"].Value.ToString();
            string strType = ShaftassyGridview.Rows[e.RowIndex].Cells[4].Value.ToString();
            string racks = ShaftassyGridview.Rows[e.RowIndex].Cells["Racks"].Value.ToString();


            //MessageBox.Show("DSDSD" + strType);

            if (e.ColumnIndex == 0)
            {
                
                EditMasterlist ml = new EditMasterlist(this, _raw);

                ml.Partnum.Text = strpartnum;
                ml.QuanText.Text =  intqty;
                ml.TypeText.Text = strType;
                ml.prodID = 1;
                ml.RacksText.Text = racks;
                ml.selectedindex = tabControl1.SelectedIndex + 1;
                ml.ShowDialog();
            }
           
        }
        private void InsertBushgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a header
            if (e.RowIndex < 0)
            {
                return;
            }

            string strpartnum = InsertBushgrid.Rows[e.RowIndex].Cells["PartNumberBush"].Value.ToString();
            string intqty = InsertBushgrid.Rows[e.RowIndex].Cells["BushQuantity"].Value.ToString();
            string racks = InsertBushgrid.Rows[e.RowIndex].Cells["RacksBush"].Value.ToString();
            
            if (e.ColumnIndex == 0)
            {
                EditMasterlist ml = new EditMasterlist(this, _raw);

                ml.Partnum.Text = strpartnum;
                ml.QuanText.Text =  intqty;
                ml.prodID = 2;
                ml.RacksText.Text = racks;
                ml.selectedindex = tabControl1.SelectedIndex + 1;
                ml.ShowDialog();
            }
        }

        
    }
}
