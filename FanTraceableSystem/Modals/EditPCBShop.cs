using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
using FanTraceableSystem.Services;
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

namespace FanTraceableSystem
{
    public partial class EditPCBShop : CustomForm
    {
        private readonly ISubassy _sub;
        public BindingList<TraceableSubAssyModel> subassy = new BindingList<TraceableSubAssyModel>();
        private string _originalShopOrder;

        private readonly HashSet<string> _shopCache = new HashSet<string>();


        // Constructor for EDIT
        public EditPCBShop(BindingList<TraceableSubAssyModel> existingList, ISubassy sub)
        {
            InitializeComponent();
            _sub = sub;

            // clone to avoid direct reference issues (recommended)
            subassy = new BindingList<TraceableSubAssyModel>(
                existingList.Select(x => new TraceableSubAssyModel
                {
                    SubAssyID = x.SubAssyID,
                    ShopOrder = x.ShopOrder,
                    PreparedQuantity = x.PreparedQuantity,
                    LotNo = x.LotNo,
                    Line = x.Line,
                    SubAssyIssued = x.SubAssyIssued,
                    Rev = x.Rev,
                    isAction = 0 // 👈 mark existing as EDIT by default (important)
                }).ToList()
            );
        }


        // =========================
        // ➕ ADD NEW ROW (FAST)
        // =========================
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Shoptext.Text))
            {
                MessageBox.Show("PCB Shop Order is required");
                return;
            }

            bool result = await _sub.CheckShopOrder(Shoptext.Text);

            if (result)
            {
                MessageBox.Show("Shop Order is Exist in the Database.");
                Shoptext.Clear();
                Shoptext.Focus();
                return;
            }



            // Example: collect values from controls
            subassy.Add(new TraceableSubAssyModel
            {
                ShopOrder = Shoptext.Text,
                PreparedQuantity = int.TryParse(QuanText.Text, out int qty) ? qty : 0,
                Rev = RevText.Text,
                LotNo = LotNotext.Text,
                Line = LineText.Text,
                SubAssyIssued = IssuerText.Text,
                isAction = 1
            });
            dataGridView1.DataSource = subassy.ToList(); // Refresh the grid    

            QuanText.Text = "";
            Shoptext.Text = "";
            LotNotext.Text = "";
            RevText.Text = "";
            LineText.Text = "";
            IssuerText.Text = "";

            Shoptext.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {


            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.IsNewRow) continue;


            //    subassy.Add(new TraceableSubAssyModel
            //    {
            //        SubAssyID = int.TryParse(row.Cells["SubAssyID"].Value?.ToString(), out int id) ? id : 0,
            //        Rev = row.Cells["Rev"].Value?.ToString(),
            //        ShopOrder = row.Cells["ShopOrder"].Value?.ToString(),
            //        SubAssyIssued = row.Cells["SubAssyIssued"].Value?.ToString(),
            //        LotNo = row.Cells["LotNo"].Value?.ToString(),
            //        Line = row.Cells["Line"].Value?.ToString(),
            //        PreparedQuantity = int.TryParse(row.Cells["PreparedQuantity"].Value?.ToString(), out int qty) ? qty : 0,
            //        isAction = int.TryParse(row.Cells["IsAction"].Value?.ToString(), out int act) ? act : 0,
            //    });
            //}

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EditPCBShop_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = subassy;
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

        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex < 0) return;

            //var row = dataGridView1.Rows[e.RowIndex].DataBoundItem as TraceableSubAssyModel;
            //if (row == null) return;

            //// Always mark as edited when changed
            //row.isAction = 2;

            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex].DataBoundItem as TraceableSubAssyModel;
            if (row == null) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ShopOrder")
            {
                string newValue = row.ShopOrder;
                Debug.WriteLine(newValue);
                bool result = await _sub.CheckShopOrder(newValue);
                Debug.WriteLine(result);
                if(!result)
                {
                    MessageBox.Show("Shop Order is Already  Exist to the other Section");
                    row.ShopOrder = _originalShopOrder; // Revert to original if invalid
                    return;
                }
                //row.isChangeShop = newValue;
                //MessageBox.Show($"Old: {_originalShopOrder} → New: {newValue}");
                // You can use _originalShopOrder for your SQL WHERE if needed
            }

            row.isAction = 2;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LineText_TextChanged(object sender, EventArgs e)
        {

        }

        private void QuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !QuanText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dataGridView1.Columns[e.ColumnIndex].Name == "ShopOrder")
            {
                _originalShopOrder = dataGridView1.Rows[e.RowIndex].Cells["ShopOrder"].Value?.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToggleUI(bool enabled)
        {
            this.Enabled = enabled;
            this.Cursor = enabled ? Cursors.Default : Cursors.WaitCursor;
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            BackgroundUpdateService.Instance.OnLog += HandleUpdateLog;
        }
        private void HandleUpdateLog(string msg)
        {
            Debug.WriteLine(msg);
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            BackgroundUpdateService.Instance.OnLog -= HandleUpdateLog;
            base.OnFormClosing(e);
        }
    }
}
