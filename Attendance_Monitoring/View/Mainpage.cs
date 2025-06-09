using Attendance_Monitoring.Global;
using Attendance_Monitoring.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace Attendance_Monitoring
{
    public partial class Mainpage : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public int sectionID;
        public string tablename;

        public Mainpage(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider=serviceProvider;
        }

        private void Moldingbtn_Click(object sender, EventArgs e)
        {
             Attendanceform(1, "M_summary");
        }

        private void Pressbtn_Click(object sender, EventArgs e)
        {
            Attendanceform(2, "P_summary");
        }

        private void Rotorbtn_Click(object sender, EventArgs e)
        {
            Attendanceform(3, "R_summary");
        }

        private void Windingbtn_Click(object sender, EventArgs e)
        {
            Attendanceform(4, "W_summary");
        }

        private void Circuitbtn_Click(object sender, EventArgs e)
        {
            Attendanceform(5, "C_summary");
        }

        private void Processbtn_Click(object sender, EventArgs e)
        {
            Attendanceform(6, "PC_summary");
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainpage = _serviceProvider.GetRequiredService<Selection>();
            // Show the main form
            mainpage.Show();
            this.Hide();
            Visible=false;
        }

        private void Employ_Click(object sender, EventArgs e)
        {
            //EmployeeManage em = new EmployeeManage();
            //em.Show();
            var mainpage = _serviceProvider.GetRequiredService<EmployeeManage>();
            // Show the main form
            mainpage.Show();
            // Hide the login form (optional)
            this.Hide();


            Visible=false;
        }

        public void Attendanceform(int sectID, string tb)
        {
            Attendance at = new Attendance(sectID, tb, _serviceProvider);
            at.Show();
            Visible = false;
        }



        private void button2_Click(object sender, EventArgs e)
        {
            double oTHours, reg;

            Timeprocess tm = new Timeprocess();




            DateTime timeIn = DateTime.Parse("2024-09-13 05:28:31.093");


            // Time in from the database
            //TimeSpan timeInSpan = TimeSpan.Parse(time["TimeIn"].ToString("HH:mm:ss"), culture);
            TimeSpan timeInSpan = timeIn.TimeOfDay;

            reg = tm.CalculateWorkingHours(timeIn.ToString(@"HH\:mm\:ss"), "14:30:00");


            MessageBox.Show("DEBUG SHOWING TIEMINE: " + reg);









            DateTime currentDatetodatu = DateTime.Now;
            string currentTime = currentDatetodatu.ToString(@"HH\:mm\:ss");
            

            oTHours = tm.CalculateOTHours("14:30:00", currentTime);

            MessageBox.Show("TIme: " + currentTime);
            MessageBox.Show("OVERTIME NO: " + oTHours);



            //DateTime timeIn = Convert.ToDateTime("2024-08-31 06:01:50.513");
          

           // TimeSpan timeInSpan = timeIn.TimeOfDay;


            MessageBox.Show("Timespan: " + timeInSpan);
        

            TimeSpan earlyStart = TimeSpan.Parse("03:30:00");
            TimeSpan lateStart = TimeSpan.Parse("06:30:00");
            TimeSpan cutoffEarly = TimeSpan.Parse("06:00:00");
            TimeSpan cutoffLate = TimeSpan.Parse("10:00:00");
            TimeSpan dayEnd = TimeSpan.Parse("14:30:00");

            if (timeInSpan >= earlyStart && timeInSpan < lateStart)
            {
                if (timeInSpan >= earlyStart && timeInSpan < cutoffEarly)
                {
                    MessageBox.Show("NO OT");
                    
                }
                else
                {
                    MessageBox.Show("HAS OVERTIME");
                    
                }
            }
            else
            {
                MessageBox.Show("NOT GOOD");
            }
           


        //  DateTime timeIn = Convert.ToDateTime("2024-08-04 05:30:25.230");
          


            


        //  TimeSpan timeInSpan = timeIn.TimeOfDay;
        //  MessageBox.Show("Date Time span: " + timeInSpan.ToString(@"hh\:mm\:ss"));

            DateTime currentDate = DateTime.Now.Date; // Gets today's date without the time
            MessageBox.Show("Date today: " + currentDate.ToString("yyyy-MM-dd"));
            //TimeSpan currentTime = DateTime.Now.TimeOfDay;
            //MessageBox.Show("Date today: " + currentTime.ToString(@"hh\:mm\:ss"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
