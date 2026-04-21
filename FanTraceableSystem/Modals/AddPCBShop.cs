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
        public List<TraceableSubAssyModel> subassy { get; private set; } = new List<TraceableSubAssyModel>();

        public AddPCBShop()
        {
            InitializeComponent();
            subassy = new List<TraceableSubAssyModel>();
        }

        // Constructor for EDIT
        public AddPCBShop(List<TraceableSubAssyModel> existingList)
        {
            InitializeComponent();

            // clone to avoid direct reference issues (recommended)
            subassy = existingList
                .Select(x => new TraceableSubAssyModel
                {
                    ShopOrder = x.ShopOrder,
                    PreparedQuantity = x.PreparedQuantity
                })
                .ToList();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            subassy.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                subassy.Add(new TraceableSubAssyModel
                {
                    ShopOrder = row.Cells["ShopOrder"].Value?.ToString(),
                    LotNo = row.Cells["LotNo"].Value?.ToString(),
                    Line = row.Cells["Line"].Value?.ToString(),
                    PreparedQuantity = int.TryParse(row.Cells["PreparedQuantity"].Value?.ToString(), out int qty) ? qty : 0,
                    Rev = row.Cells["Rev"].Value?.ToString(),
                    SubAssyIssued = row.Cells["SubAssyIssued"].Value?.ToString()
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
            subassy.Add(new TraceableSubAssyModel
            {
                ShopOrder = Shoptext.Text,
                LotNo = LotText.Text,
                SubAssyIssued = IssuerText.Text,    
                PreparedQuantity = int.TryParse(textBox1.Text, out int qty) ? qty : 0,
                Line = LineText.Text,
                Rev = RevText.Text
            });
            dataGridView1.DataSource = subassy.ToList(); // Refresh the grid    

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
            dataGridView1.DataSource = subassy;
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
