using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductConfirm.Modules
{
    public partial class Shoporders : UserControl
    {

        public delegate void PassData_CallTo(string textBoxData);

        //public DataGridView MyDataGridView
        // {
        //    get { return dataGridView1; } // Replace 'dataGridView1' with your actual DataGridView name
        // }


        public Shoporders()
        {
            InitializeComponent();
           
        }

        

        private void Shoporders_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadData(DataTable dataTable)
        {
           // MyDataGridView.DataSource = dataTable;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
           // MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        public void textBoxChange(string input) //change the display of the textBox
        {
            textBox1.Text = input;
        }
    }
}
