using Parts_locator.Data;
using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush.Modules
{
    public partial class BushMasterlist : UserControl
    {
        public DataGridView shafttable { get { return ShaftassyGridview; } }
        public int selectint;


        public BushMasterlist()
        {
            InitializeComponent();
        }


        public void UpdateDisplayTable(int selected)
        {
            DataTable table = new DataTable();
            selectint = tabControl1.SelectedIndex + 1;

            switch (selectint)
            {
                case 1:
                    table =  ProductsMolding.getModelingRowByType(1);
                    ShaftassyGridview.DataSource = table;
                    ShaftassyGridview.Columns["Edit"].DisplayIndex = 5;
                    break;
                case 2:
                    table =  ProductsMolding.getModelingRowByType(2);
                    InsertBushgrid.DataSource = table;
                    InsertBushgrid.Columns["EditFrame"].DisplayIndex = 4;
                    break;
            }
        }


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {    
            selectint = tabControl1.SelectedIndex + 1;
            UpdateDisplayTable(selectint);
        }

        private void ShaftassyGridview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a header
            if (e.RowIndex < 0)
            {
                // Do nothing or handle the header click as needed
                return;
            }

            string strpartnum = ShaftassyGridview.Rows[e.RowIndex].Cells["PartNumbershaft"].Value.ToString();
            string intqty = ShaftassyGridview.Rows[e.RowIndex].Cells["ShaftQuantity"].Value.ToString();
            string strType = ShaftassyGridview.Rows[e.RowIndex].Cells[4].Value.ToString();
            string racks = ShaftassyGridview.Rows[e.RowIndex].Cells["Racks"].Value.ToString();


            //MessageBox.Show("DSDSD" + strType);

            if (e.ColumnIndex == 0)
            {
                
                EditMasterlist ml = new EditMasterlist(this);

                ml.Partnum.Text = strpartnum;
                ml.QuanText.Text =  intqty;
                ml.TypeText.Text = strType;
                ml.prodID = 1;
                ml.RacksText.Text = racks;
                ml.selectedindex = selectint;
                ml.ShowDialog();
            }
           
        }

        private void InsertBushgrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the click is on a header
            if (e.RowIndex < 0)
            {
                // Do nothing or handle the header click as needed
                return;
            }

            string strpartnum = InsertBushgrid.Rows[e.RowIndex].Cells["PartNumberBush"].Value.ToString();
            string intqty = InsertBushgrid.Rows[e.RowIndex].Cells["BushQuantity"].Value.ToString();
            string racks = InsertBushgrid.Rows[e.RowIndex].Cells["RacksBush"].Value.ToString();
            
            if (e.ColumnIndex == 0)
            {
                EditMasterlist ml = new EditMasterlist(this);

                ml.Partnum.Text = strpartnum;
                ml.QuanText.Text =  intqty;
                ml.prodID = 2;
                ml.RacksText.Text = racks;
                ml.selectedindex = selectint;
                ml.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateDisplayTable(1);
        }

        private void ShaftassyGridview_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BushMasterlist_Load(object sender, EventArgs e)
        {

        }
    }
}
