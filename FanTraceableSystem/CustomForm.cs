using FanTraceableSystem.Services;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace FanTraceableSystem
{
    public partial class CustomForm : Form
    {
        public CustomForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Prevent Designer crash
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            if (DesignMode)
                return;

            if (BackgroundUpdateService.IsInitialized)
            {
                BackgroundUpdateService.Instance.OnLog += HandleUpdateLog;
                BackgroundUpdateService.Instance.OnUpdateStarted += HandleUpdateStarted;
                BackgroundUpdateService.Instance.OnUpdateCompleted += HandleUpdateCompleted;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Prevent Designer crash
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                base.OnFormClosing(e);
                return;
            }

            if (BackgroundUpdateService.IsInitialized)
            {
                BackgroundUpdateService.Instance.OnLog -= HandleUpdateLog;
                BackgroundUpdateService.Instance.OnUpdateStarted -= HandleUpdateStarted;
                BackgroundUpdateService.Instance.OnUpdateCompleted -= HandleUpdateCompleted;
            }

            base.OnFormClosing(e);
        }

        private void HandleUpdateLog(string msg)
        {
            Debug.WriteLine(msg);
        }

        private void HandleUpdateStarted(Version current, Version latest)
        {
            Debug.WriteLine($"Updating {current} -> {latest}");
        }

        private void HandleUpdateCompleted()
        {
            Debug.WriteLine("Update completed");
        }
    }
}
