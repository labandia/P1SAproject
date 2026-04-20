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
    public partial class EditPCBShop : Form
    {
        public BindingList<EditTracePCBModel> PCBList = new BindingList<EditTracePCBModel>();

        // Constructor for EDIT
        public EditPCBShop(BindingList<EditTracePCBModel> existingList)
        {
            InitializeComponent();

            // clone to avoid direct reference issues (recommended)
            PCBList = new BindingList<EditTracePCBModel>(
                existingList.Select(x => new EditTracePCBModel
                {
                    RecordId = x.RecordId,
                    PCBShopOrder = x.PCBShopOrder,
                    Quantity = x.Quantity,
                    LotNo = x.LotNo,
                    Line = x.Line,
                    PCBIssuer = x.PCBIssuer,
                    Rev = x.Rev,
                    isAction = 1 // 👈 mark existing as EDIT by default (important)
                }).ToList()
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Shoptext.Text))
            {
                MessageBox.Show("PCB Shop Order is required");
                return;
            }


            // Example: collect values from controls
            PCBList.Add(new EditTracePCBModel
            {
                PCBShopOrder = Shoptext.Text,
                Quantity = int.TryParse(QuanText.Text, out int qty) ? qty : 0,
                Rev = RevText.Text,
                LotNo = LotNotext.Text,
                Line = LineText.Text,
                PCBIssuer = IssuerText.Text,
                isAction = 0
            });
            dataGridView1.DataSource = PCBList.ToList(); // Refresh the grid    

            QuanText.Text = "";
            Shoptext.Text = "";
            LotNotext.Text = "";


            Shoptext.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

            PCBList.Clear();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                PCBList.Add(new EditTracePCBModel
                {
                    RecordId = int.TryParse(row.Cells["RecordId"].Value?.ToString(), out int id) ? id : 0,
                    Rev = row.Cells["Rev"].Value?.ToString(),
                    PCBShopOrder = row.Cells["PCBShopOrder"].Value?.ToString(),
                    PCBIssuer = row.Cells["PCBIssuer"].Value?.ToString(),   
                    LotNo = row.Cells["LotNo"].Value?.ToString(),
                    Line = row.Cells["Line"].Value?.ToString(),
                    Quantity = int.TryParse(row.Cells["Quantity"].Value?.ToString(), out int qty) ? qty : 0,
                    isAction = int.TryParse(row.Cells["IsAction"].Value?.ToString(), out int act) ? act : 0,
                });
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EditPCBShop_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = PCBList;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RevText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex].DataBoundItem as EditTracePCBModel;
            if (row == null) return;

            if (row.isAction == 0)
            {
                // newly added → keep as ADD
                return;
            }

            // existing row → mark as edited
            row.isAction = 1;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
