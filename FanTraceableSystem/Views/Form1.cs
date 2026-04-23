using FanTraceableSystem.Interface;
using FanTraceableSystem.Modals;
using FanTraceableSystem.Services;
using System;
using System.Deployment.Application;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class Form1 : Form
    {
        private readonly ITraceable _trac;
        private readonly ISubassy _sub;
        private readonly ISummary _summary;

        public Form1(ITraceable traceable, ISummary sum, ISubassy sub)
        {
            InitializeComponent();
            _trac = traceable;
            _summary = sum;
            _sub = sub;

            //StartVersionCheck();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void ShowPopup(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ShowPopup(message)));
                return;
            }

            var notif = new SystemUpdaterNotification();
            notif.Show();
        }


        private async Task<string> CheckClickOnceUpdate()
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
                return null; // Not installed via ClickOnce

            try
            {
                var deployment = ApplicationDeployment.CurrentDeployment;

                // Check for update (async wrapper)
                var updateInfo = await Task.Run(() => deployment.CheckForDetailedUpdate());

                if (updateInfo.UpdateAvailable)
                {
                    // Optional: you can log instead of notify
                    Console.WriteLine($"Updating to {updateInfo.AvailableVersion}...");

                    // Silent update (no UI)
                    deployment.Update();

                    // Restart safely on UI thread
                    Invoke(new Action(() =>
                    {
                        Application.Restart();
                    }));

                    return $"New version available!\nCurrent: {updateInfo}\nLatest: {updateInfo}";
                }
            }
            catch (DeploymentDownloadException)
            {
                return "Cannot check for updates (network issue)";
            }
            catch (InvalidDeploymentException)
            {
                return "Invalid ClickOnce deployment";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }

        private async void StartVersionCheck()
        {
            while (true)
            {
                var result = await CheckClickOnceUpdate();

                if (!string.IsNullOrEmpty(result))
                {
                    ShowPopup(result); // your notification form
                }

                await Task.Delay(60000); // check every 1 minute
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //await ClickOnceUpdateManager.CheckAndUpdateAsync(ShowPopup);
        }
    }
}
