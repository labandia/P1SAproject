using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using MSDMonitoring.View.Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MSDMonitoring
{
    public partial class MSDMasterlist : Form
    {
        private readonly IMSD _msd;
        private List<MSDMasterlistodel> _masterData = new List<MSDMasterlistodel>();

        public MSDMasterlist(IMSD msd)
        {
            InitializeComponent();
            _msd = msd;

            MonitorTable.CellContentClick += MonitorTable_CellContentClick;
        }

        public async Task DisplayData()
        {
            _masterData = await _msd.GetMSDMasterlist();
            MonitorTable.DataSource = _masterData;    
        }

        private async void MSDMasterlist_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable; // keeps title bar

            await DisplayData();
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void MonitorTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void Exportbtn_Click(object sender, EventArgs e)
        {
            AddMasterList add = new AddMasterList(_msd, this);
            add.ShowDialog();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            string filterText = searchBox.Text.ToLower();
            var filteredList = new List<MSDMasterlistodel>();


            filteredList = _masterData.Where(p => p.AmbassadorPartnum.ToLower().Contains(filterText)).ToList();
            MonitorTable.DataSource =  filteredList;
        }

        private void MonitorTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // Ignore header

            int editColumnIndex = 0; // 7th column (zero-based)

            if (e.ColumnIndex == editColumnIndex)
            {

                var obj = new MSDMasterlistodel
                {
                    AmbassadorPartnum = MonitorTable.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    Partname = MonitorTable.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    SupplyPartName = MonitorTable.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    SupplyName = MonitorTable.Rows[e.RowIndex].Cells[4].Value.ToString(),
                    Level = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[5].Value),
                    FloorLife = Convert.ToInt32(MonitorTable.Rows[e.RowIndex].Cells[6].Value),
                };

                EditMasterlist ed = new EditMasterlist(_msd, this, obj);
                ed.ShowDialog();
            }
        }
    }
}
