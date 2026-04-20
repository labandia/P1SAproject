using FanTraceableSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class AddPCBShop : Form
    {
        public List<TracePCBModel> PCBList { get; private set; } = new List<TracePCBModel>();

        public AddPCBShop()
        {
            InitializeComponent();
            PCBList = new List<TracePCBModel>();
        }

        // Constructor for EDIT
        public AddPCBShop(List<TracePCBModel> existingList)
        {
            InitializeComponent();

            // clone to avoid direct reference issues (recommended)
            PCBList = existingList
                .Select(x => new TracePCBModel
                {
                    PCBShopOrder = x.PCBShopOrder,
                    Quantity = x.Quantity
                })
                .ToList();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
       
            PCBList.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                PCBList.Add(new TracePCBModel
                {
                    LotNo = row.Cells["LotNo"].Value?.ToString(),
                    PCBShopOrder = row.Cells["PCBShopOrder"].Value?.ToString(),
                    Quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty) ? qty : 0,
                    Rev = row.Cells["Rev"].Value?.ToString()    
                });
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // this is to add to the datagrid view from the addPCBshop form
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Shoptext.Text))
            {
                MessageBox.Show("Shop Order is required");
                return;
            }



            //if (PCBList.Any(x => x.PCBShopOrder == Shoptext.Text))
            //{
            //    MessageBox.Show("Duplicate PCB Shop Order");
            //    return;
            //}

            // Example: collect values from controls
            PCBList.Add(new TracePCBModel
            {
                LotNo = LotText.Text,
                PCBShopOrder = Shoptext.Text,
                PCBIssuer = IssuerText.Text,    
                Quantity = int.TryParse(textBox1.Text, out int qty) ? qty : 0,
                Line = LineText.Text,
                Rev = RevText.Text
            });
            dataGridView1.DataSource = PCBList.ToList(); // Refresh the grid    

            textBox1.Text = "";
            Shoptext.Text = "";
            LotText.Text = "";
            textBox1.Text = ""; 
            RevText.Text = "";  
            LineText.Text = "";
            IssuerText.Text = "";

            Shoptext.Focus();
        }

        private void AddPCBShop_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = PCBList;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !textBox1.Text.Contains("."))) ? false : true; // Allow the character
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
