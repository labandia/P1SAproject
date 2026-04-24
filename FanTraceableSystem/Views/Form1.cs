using FanTraceableSystem.Interface;
using FanTraceableSystem.Modals;
using FanTraceableSystem.Services;
using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class Form1 : Form
    {
        private BackgroundUpdateService _updater;

        private System.Windows.Forms.Timer _updateTimer;
        private bool _isCheckingUpdate = false;

        private readonly ITraceable _trac;
        private readonly ISubassy _sub;
        private readonly ISummary _summary;

        public Form1(ITraceable traceable, ISummary sum, ISubassy sub)
        {
            InitializeComponent();
            _trac = traceable;
            _summary = sum;
            _sub = sub;

            _updater = new BackgroundUpdateService(
               @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\SystemVersion\SubAssyVersion.txt",
               TimeSpan.FromSeconds(5) // change to minutes in production
           );

            _updater.OnLog += msg =>
            {
                System.Diagnostics.Debug.WriteLine(msg);
            };

            _updater.OnUpdateStarted += (current, next) =>
            {
                System.Diagnostics.Debug.WriteLine($"Updating {current} → {next}");
            };

            _updater.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var openinput = new TraceableHistory(_summary);
            openinput.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemUpdaterNotification updater = new SystemUpdaterNotification();
            updater.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(1))
                return;
            SelectionSection(1);
           
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(2))
                return;
            SelectionSection(2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(3))
                return;
            SelectionSection(3);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(4))
                return;
            SelectionSection(4);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(5))
                return;
            SelectionSection(5);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(6))
                return;
            SelectionSection(6);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!AuthHelper.RequiredPassword(7))
                return;
            SelectionSection(7);
        }


        public void SelectionSection(int section)
        {
            var openinput = new FanTraceabilityAutoSearch(_trac, _sub, section);

            openinput.Owner = this;   // set current form as parent
            openinput.Show();

            this.Hide(); // or Close() depending on your flow
        }

        private void ShowPopup(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowPopup(message)));
                return;
            }

            //MessageBox.Show("Update System Completed");
            //var notif = new SystemUpdaterNotification();
            //notif.Show();
        }

        private void StartVersionCheck()
        {
            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 5000; // 5 seconds

            _updateTimer.Tick += async (s, e) =>
            {
                // 🚫 Prevent overlapping calls
                if (_isCheckingUpdate) return;

                try
                {
                    _isCheckingUpdate = true;

                    await ClickOnceUpdateManager.CheckAndUpdateAsync(msg =>
                    {
                        Debug.WriteLine(msg);

                        // Optional UI notification
                        // ShowPopup(msg);
                    });
                }
                finally
                {
                    _isCheckingUpdate = false;
                }
            };

            _updateTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label4.Text = "Version" + GetPublishedVersion();
        }

        protected override async void OnFormClosing(FormClosingEventArgs e)
        {
            if (_updater != null)
                await _updater.StopAsync();

            base.OnFormClosing(e);
        }


        public string GetPublishedVersion()
        {
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return version.ToString();
            }

            return "Not a ClickOnce deployed application";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
           Application.Exit();    
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
