using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class ComponentsOut : Form
    {
        private readonly MSDstartup _msdform;
        private readonly IMSD _msd;

        public string strTimeIN;
        public string strReelID;
        public string strstartTime;
        public int setQuantity;

        public double setFloorlife;
        public double getFloorlife;
        public int _ID { get; set; }

        Timer timer;

        public ComponentsOut(MSDstartup msdform, IMSD msd, int ID)
        {
            InitializeComponent();
            _msdform = msdform;
            _msd = msd;
            _ID = ID;

        }

        // ========================================================================== //
        // =================== LOAD DATA FUNCTIONALITY  ============================= //
        // ========================================================================== //
        public async Task GetDetails(int ID)
        {
            var data = await _msd.GetListComponentIN();
            var filterdata = data.SingleOrDefault(res => res.RecordID == ID);
            if (filterdata != null)
            {
                strTimeIN = filterdata.DateCheck;
                setFloorlife = filterdata.RemainFloor;
                strstartTime = filterdata.DateCheck;
                ReelText.Text = filterdata.ReelID;
                strReelID = filterdata.ReelID;  
                DateText.Text = "Date IN : " + filterdata.DateIn;
                TimeText.Text = "Time IN : " + filterdata.TimeIn;
                QuanText.Text = "Reel Quantity : " + filterdata.QuantityIN.ToString();
                setQuantity = filterdata.QuantityIN;
                LineText.Text = "Line : " + filterdata.Line.ToString();
            }
          
        }
        private async void ComponentsOut_Load(object sender, EventArgs e)
        {
            await GetDetails(_ID);
            SetupTimer();
        }
        // ========================================================================== //
        // ========================= UPDATE FUNCTIONALITY =========================== //
        // ========================================================================== //
        private async void updatebtn_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = DateTime.Now;

            Debug.WriteLine("Date : " + strTimeIN);

            DateTime startTime = DateTime.ParseExact(
                strTimeIN,
                "MM/dd/yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture
            );

            string strEndTime  = selectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff");
            DateTime endTime = DateTime.ParseExact(strEndTime, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

            decimal elapsedHours = (decimal)(endTime - startTime).TotalSeconds / 3600m;



            if (FormValidation())
            {
                int getQuan = Convert.ToInt32(QuantityInput.Text);
                if (getQuan == 0) getFloorlife = 0;

                int remainQuan = setQuantity - Convert.ToInt32(QuantityInput.Text);

                // Update the ReelChecker
                await _msd.UpdateChecker(ReelText.Text, getFloorlife, remainQuan);

                // Updates the History Data
                var obj = new InputOUT_MSD
                {
                    RecordID = _ID,
                    DateOut = selectedDate.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                    QuantityOut = String.IsNullOrEmpty(QuantityInput.Text) ? 0 : Convert.ToInt32(QuantityInput.Text),
                    INputOut =  String.IsNullOrEmpty(NameInput.Text) ? "" : NameInput.Text,
                    RemainFloor = getFloorlife,
                    IsStats = 1
                };


                bool result = await _msd.UpdateComponentsData(obj, strReelID, elapsedHours);

                if (result)
                {
                    MessageBox.Show("Updated successfully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("MSD History is Updated", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await _msdform.LoadData();
                    this.Close();
                }
            }
        }
        // ========================================================================== //
        // ============= TIME CLOCK FOR REMAING FLOORLIFE =========================== //
        // ========================================================================== //
        private void RemainClock_Tick(object sender, EventArgs e)
        {
            string strstartTimeV = strstartTime;  // MM/dd/yyyy HH:mm:ss
            double strFloorlife = setFloorlife; // Floor life in hours

            // Parse the start time with matching format
            DateTime startTime = DateTime.ParseExact(
                strstartTimeV,
                "MM/dd/yyyy HH:mm:ss",
                System.Globalization.CultureInfo.InvariantCulture
            );

            // Current time
            DateTime endTime = DateTime.Now;

            TimeRunner.Text = "Current Time :" + endTime.ToString("HH:mm:ss");

            // Calculate elapsed hours
            decimal elapsedHours = (decimal)(endTime - startTime).TotalSeconds / 3600m;

            // Remaining floor life
            double remainingFloorLife = strFloorlife - Math.Round((double)elapsedHours, 2);

            if (remainingFloorLife < 0)
                remainingFloorLife = 0;

            // Show in label
            RemainText.Text = $"{remainingFloorLife} hours";
            getFloorlife = remainingFloorLife;
        }
        private void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 1000; // 1 second
            timer.Tick += RemainClock_Tick;
            timer.Start();
        }


        public bool FormValidation()
        {
            if (string.IsNullOrEmpty(NameInput.Text))
            {
                ReelID_error.Visible = string.IsNullOrEmpty(NameInput.Text) ? true : false;
                return false;
            }


            int remainQuan = setQuantity - Convert.ToInt32(QuantityInput.Text);

            if(remainQuan < 0)
            {
                MessageBox.Show("Invalid Input Quantity", "Input Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void QuantityInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }
    }
}
