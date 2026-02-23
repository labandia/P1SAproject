using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using POS_System.Services;

namespace POS_System
{
    public partial class SalesHistoryPage : Form
    {
        private SaleService saleService = new SaleService();
        private List<Sale> allSales = new List<Sale>();

        public SalesHistoryPage()
        {
            InitializeComponent();
        }

        public async void LoadSalesAsync()
        {
            allSales = await saleService.LoadSalesAsync();
            productsTable.DataSource = allSales;
        }

        private void SalesHistoryPage_Load(object sender, EventArgs e)
        {
            LoadSalesAsync();
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


            // ✅ LOAD MONTH VIEW FIRST
            LoadMonth(DateTime.Now.Month);
            SetActive(monthbtn);
        }


        private void LoadToday()
        {
            var summary = saleService.GetTodaySummary();
            UpdateSummaryUI(summary);

            var bars = saleService.GetHourlySalesToday();
            DisplayBarChart(bars);
        }

        private void LoadWeek()
        {
            var summary = saleService.GetWeekSummary();
            UpdateSummaryUI(summary);

            var bars = saleService.GetDailySalesThisWeek();
            DisplayBarChart(bars);
        }
        private void LoadMonth(int month)
        {
            var summary = saleService.GetMonthSummary(month);
            UpdateSummaryUI(summary);

            var bars = saleService.GetDailySalesByMonth(month);
            DisplayBarChart(bars);
        }

        private void UpdateSummaryUI(SalesSummaryModel s)
        {
            lblRevenue.Text = s.TotalRevenue.ToString("₱#,##0.00");
            //lblOrders.Text = s.TotalOrders.ToString();
            lblAverage.Text = s.AverageSale.ToString("₱#,##0.00");
            lblUnits.Text = s.TotalUnits.ToString();
        }

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

        private void SetActive(Button btn)
        {
            Todaybtn.BackColor = SystemColors.Control;
            WeekBtn.BackColor = SystemColors.Control;
            monthbtn.BackColor = SystemColors.Control;

            btn.BackColor = Color.Orange;
        }

        private void cmbMonthFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cmbMonthFilter.SelectedValue == null) return;

            //int month = (int)cmbMonthFilter.SelectedValue;
            //LoadMonth(month);
            //SetActive(monthbtn);
        }


        private void DisplayBarChart(List<BarGraphPoint> data)
        {
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            ChartArea area = new ChartArea("MainArea");
            area.AxisX.Interval = 1;
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisY.MajorGrid.LineColor = Color.LightGray;
            area.AxisY.LabelStyle.Format = "₱#,##0";

            chart1.ChartAreas.Add(area);

            Series series = new Series("Sales")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String,
                YValueType = ChartValueType.Double,
                IsValueShownAsLabel = true
            };

            series.LabelFormat = "₱#,##0.00";

            foreach (var item in data)
            {
                series.Points.AddXY(item.Label, item.Total);
            }

            chart1.Series.Add(series);
        }
    }
}
