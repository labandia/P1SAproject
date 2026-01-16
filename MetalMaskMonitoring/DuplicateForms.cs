using MetalMaskMonitoring.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetalMaskMonitoring
{
    public partial class DuplicateForms : Form
    {
        private readonly List<MetalMaskModel> _data;

        public int selectedID;

        public DuplicateForms(List<MetalMaskModel> data)
        {
            InitializeComponent();
            _data = data;   
        }

        private void DuplicateForms_Load(object sender, EventArgs e)
        {
            MetalMaskTable.DataSource = _data;  
        }

        private void MetalMaskTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(MetalMaskTable.Rows[e.RowIndex].Cells["RecordID"].Value);
            selectedID = id;

            this.DialogResult = DialogResult.OK;
        }
    }
}
