using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Web.ApplicationServices;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using POS_System.Services;

namespace POS_System
{
    public partial class SalesHistoryPage : Form
    {
        private readonly SaleService saleService = new SaleService();
        private List<Sale> allSales = new List<Sale>();
        public SalesHistoryPage()
        {
            InitializeComponent();
        }


        // ================================
        // LOAD DATA (SORT ONCE ONLY)
        // ================================
        public async Task LoadSalesAsync()
        {
            allSales = (await saleService.LoadSalesAsync())
                        .OrderByDescending(x => x.SaleID)
                        .ToList();

            productsTable.DataSource = allSales;
        }

        // ================================
        // FORM LOAD
        // ================================
        private async void SalesHistoryPage_Load(object sender, EventArgs e)
        {
            SetupChart();

            await LoadSalesAsync();

            cmbMonthFilter.DataSource = Enumerable.Range(1, 12)
                .Select(m => new
                {
                    MonthNo = m,
                    MonthName = new DateTime(2000, m, 1).ToString("MMMM")
                })
                .ToList();

            cmbMonthFilter.DisplayMember = "MonthName";
            cmbMonthFilter.ValueMember = "MonthNo";
            cmbMonthFilter.SelectedValue = DateTime.Now.Month;

            LoadMonth(DateTime.Now.Month);
            SetActive(monthbtn);
        }

        // ================================
        // CORE PROCESSOR (SINGLE PASS)
        // ================================
        private void ProcessSales(IEnumerable<Sale> filtered,
                                  Func<Sale, string> chartKeySelector)
        {
            decimal revenue = 0;
            int units = 0;
            var invoiceSet = new HashSet<string>();
            var chartData = new Dictionary<string, decimal>();

            foreach (var sale in filtered)
            {
                revenue += sale.Total;
                units += sale.Quantity;
                invoiceSet.Add(sale.InvoiceNo);

                string key = chartKeySelector(sale);

                if (chartData.ContainsKey(key))
                    chartData[key] += sale.Total;
                else
                    chartData[key] = sale.Total;
            }

            // Update Grid
            productsTable.DataSource = filtered.ToList();

            // Update Summary
            var summary = new SalesSummaryModel
            {
                TotalRevenue = revenue,
                TotalUnits = units,
                TotalOrders = invoiceSet.Count
            };

            UpdateSummaryUI(summary);

            // Update Chart
            var bars = chartData
                .OrderBy(x => x.Key)
                .Select(x => new BarGraphPoint
                {
                    Label = x.Key,
                    Total = x.Value
                })
                .ToList();

            DisplayBarChart(bars);
        }

        // ================================
        // TODAY
        // ================================
        private void LoadToday()
        {
            SalesText.Text = "Today's Sales";

            DateTime today = DateTime.Today;

            var filtered = allSales
                .Where(x => x.Date.Date == today);

            ProcessSales(filtered,
                x => x.Date.Hour + ":00");
        }

        // ================================
        // WEEK
        // ================================
        private void LoadWeek()
        {
            SalesText.Text = "Weekly Sales";

            DateTime startOfWeek =
                DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);

            var filtered = allSales
                .Where(x => x.Date.Date >= startOfWeek);

            ProcessSales(filtered,
                x => x.Date.DayOfWeek.ToString());
        }

        // ================================
        // MONTH
        // ================================
        private void LoadMonth(int month)
        {
            SalesText.Text = "Monthly Sales";

            int year = DateTime.Now.Year;

            var filtered = allSales
                .Where(x => x.Date.Month == month &&
                            x.Date.Year == year);

            ProcessSales(filtered,
                x => x.Date.Day.ToString());
        }

        // ================================
        // SUMMARY UI
        // ================================
        private void UpdateSummaryUI(SalesSummaryModel s)
        {
            lblRevenue.Text = s.TotalRevenue.ToString("₱#,##0.00");
            lblAverage.Text = s.AverageSale.ToString("₱#,##0.00");
            lblUnits.Text = s.TotalUnits.ToString();
        }

        // ================================
        // BUTTON EVENTS
        // ================================
        private void Todaybtn_Click(object sender, EventArgs e)
        {
            LoadToday();
            SetActive(Todaybtn);
        }

        private void WeekBtn_Click(object sender, EventArgs e)
        {
            LoadWeek();
            SetActive(WeekBtn);
        }

        private void monthbtn_Click(object sender, EventArgs e)
        {
            int month = (int)cmbMonthFilter.SelectedValue;
            LoadMonth(month);
            SetActive(monthbtn);
        }
        private void cmbMonthFilter_SelectedIndexChanged(object sender, EventArgs e)
        { //if (cmbMonthFilter.SelectedValue == null) return; //int month = (int)cmbMonthFilter.SelectedValue; //LoadMonth(month); //SetActive(monthbtn); }
        }
            private void SetActive(Button btn)
        {
            Todaybtn.BackColor = SystemColors.Control;
            WeekBtn.BackColor = SystemColors.Control;
            monthbtn.BackColor = SystemColors.Control;

            btn.BackColor = Color.Orange;
        }

        // ================================
        // CHART SETUP (RUN ONCE)
        // ================================
        private void SetupChart()
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            var area = new ChartArea("MainArea");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.LabelStyle.Format = "₱#,##0";

            chart1.ChartAreas.Add(area);

            var series = new Series("Sales")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                IsValueShownAsLabel = true,
                LabelFormat = "₱#,##0.00"
            };

            chart1.Series.Add(series);
        }

        // ================================
        // CHART UPDATE (FAST)
        // ================================
        private void DisplayBarChart(List<BarGraphPoint> data)
        {
            var series = chart1.Series["Sales"];
            series.Points.Clear();

            foreach (var item in data)
            {
                series.Points.AddXY(item.Label, item.Total);
            }
        }

    }
}
