using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring
{
    public partial class ComponentsOut : Form
    {
        private readonly MSDstartup _msdform;
        private readonly IMSD _msd;

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
                setFloorlife = filterdata.RemainFloor;
                strstartTime = filterdata.DateCheck;
                ReelText.Text = filterdata.ReelID;
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

            if (FormValidation())
            {

                int remainQuan = setQuantity - Convert.ToInt32(QuantityInput.Text);

                // Update the ReelChecker
                await _msd.UpdateChecker(ReelText.Text, getFloorlife, remainQuan);

                DateTime selectedDate = DateTime.Now;


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


                bool result = await _msd.UpdateComponentsData(obj);

                if (result)
                {
                    MessageBox.Show("Update successfully");
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
            int remainQuan = setQuantity - Convert.ToInt32(QuantityInput.Text);

            if(remainQuan < 0)
            {
                MessageBox.Show("Invalid Input");
                return false;
            }

            return true;
        }
        private void cancelbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
