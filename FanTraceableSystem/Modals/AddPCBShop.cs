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
    public partial class AddPCBShop : Form
    {
        private readonly ISubassy _sub;

        // Use BindingList for automatic UI updates
        public BindingList<TraceableSubAssyModel> subassy { get; private set; }

        // FOR ADD ONLY 
        public AddPCBShop(ISubassy sub)
        {
            InitializeComponent();
            _sub = sub;
            subassy = new BindingList<TraceableSubAssyModel>();
        }

        // FOR EDIT ONLY
        public AddPCBShop(BindingList<TraceableSubAssyModel> existingList)
        {
            InitializeComponent();

            // clone to avoid direct reference issues (recommended)
            subassy = new BindingList<TraceableSubAssyModel>(
            existingList.Select(x => new TraceableSubAssyModel
            {
                ShopOrder = x.ShopOrder,
                LotNo = x.LotNo,
                Line = x.Line,
                PreparedQuantity = x.PreparedQuantity,
                Rev = x.Rev,
                SubAssyIssued = x.SubAssyIssued
            }).ToList()
        );
        }

        private void AddPCBShop_Load(object sender, EventArgs e)
        {
            FormReset();
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = subassy;
        }


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            // If you still want to ensure sync with grid:
            subassy = new BindingList<TraceableSubAssyModel>(
                dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => !r.IsNewRow)
                    .Select(CreateModelFromRow)
                    .ToList()
            );

            DialogResult = DialogResult.OK;
            Close();
        }

        // this is to add to the datagrid view from the addPCBshop form
        private  void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Shoptext.Text))
            {
                MessageBox.Show("Shop Order is required");
                return;
            }
            // Example: collect values from controls
            subassy.Add(CreateformSubmit());
            dataGridView1.DataSource = subassy.ToList(); // Refresh the grid    

            FormReset();
            Shoptext.Focus();
        }

       

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !textBox1.Text.Contains("."))) ? false : true; // Allow the character
        }

  

        private async void Shoptext_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            await checkShop();
        }

        public async Task checkShop()
        {
            bool result = await _sub.CheckShopOrder(Shoptext.Text);

            if (!result)
            {
                MessageBox.Show("Shop Order is Exist in the Database.");
                Shoptext.Clear();
                Shoptext.Focus();
                return;
            }
        }

        private TraceableSubAssyModel CreateformSubmit()
        {
            return new TraceableSubAssyModel
            {
                ShopOrder = Shoptext.Text.Trim(),
                LotNo = LotText.Text.Trim(),
                SubAssyIssued = IssuerText.Text,
                PreparedQuantity = int.TryParse(textBox1.Text, out int qty) ? qty : 0,
                Line = LineText.Text.Trim(),
                Rev = RevText.Text
            };
        }
        // 🔹 Extracted: Create model from grid row
        private TraceableSubAssyModel CreateModelFromRow(DataGridViewRow row)
        {
            return new TraceableSubAssyModel
            {
                ShopOrder = row.Cells["ShopOrder"].Value?.ToString(),
                LotNo = row.Cells["LotNo"].Value?.ToString(),
                Line = row.Cells["Line"].Value?.ToString(),
                PreparedQuantity = ParseInt(row.Cells["PreparedQuantity"].Value?.ToString()),
                Rev = row.Cells["Rev"].Value?.ToString(),
                SubAssyIssued = row.Cells["SubAssyIssued"].Value?.ToString()
            };
        }

        private int ParseInt(string value)
        {
            return int.TryParse(value, out int qty) ? qty : 0;
        }

        // ✅ Cleaner reset
        private void FormReset()
        {
            foreach (Control ctrl in Controls)
            {
                if (ctrl is TextBox tb)
                    tb.Clear();
            }
        }


        private void button2_Click(object sender, EventArgs e) => this.Close();
        private void button3_Click(object sender, EventArgs e) => this.Close();


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
