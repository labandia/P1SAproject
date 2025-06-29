using Attendance_Monitoring.Controller;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Attendance_Monitoring.Usercontrols
{
    public partial class AttendancePage : UserControl
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AdminController _admin;
        //private static List<AttendanceModel> itemattends;
        //private static List<Employee> emplist;
   

        private readonly Timer timer;

        // Share variable to all
        //public int sec;
        //public string tb;
        public string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");


        public int DepartmentID { get; set; }

        public AttendancePage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            //sec = section;
            //tb = tablename;
            _admin = new AdminController();
            _serviceProvider=serviceProvider;
        }

        
      


        private async void EnterTime(object sender, KeyEventArgs e)
        {
            MessageBox.Show("Working Inside the usercontrol" + DepartmentID.ToString());
            await Task.Delay(200);
        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            // Stop the timer after it has ticked
            timer.Stop();

            // Code to execute after timeout
            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "Checking status ...";
            EmployID.Text = "";
        }
        private void TimerOut_Tick(object sender, EventArgs e)
        {
            // Stop the timer after it has ticked
            timer.Stop();

            // Code to execute after timeout
            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "Checking status ...";
            EmployID.Text = "";
            TextName.Text = "";
            EmployID.Focus();
        }

        public void InitializePage()
        {
            MessageBox.Show("Running after set: " + DepartmentID);
            // Now you can load employees, etc.
        }

    }
}
