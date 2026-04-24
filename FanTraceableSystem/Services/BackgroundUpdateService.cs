using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FanTraceableSystem.Services
{
    public class BackgroundUpdateService : IDisposable
    {
        private readonly string _networkFile;
        private readonly TimeSpan _interval;
        private CancellationTokenSource _cts;
        private Task _workerTask;

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);

        public event Action<string> OnLog;
        public event Action<Version, Version> OnUpdateStarted;

        public BackgroundUpdateService(string networkFile, TimeSpan interval)
        {
            _networkFile = networkFile;
            _interval = interval;
        }

        public void Start()
        {
            if (_workerTask != null) return;

            _cts = new CancellationTokenSource();
            _workerTask = Task.Run(() => RunAsync(_cts.Token));
        }

        public async Task StopAsync()
        {
            if (_cts == null) return;

            _cts.Cancel();

            try
            {
                await _workerTask;
            }
            catch (TaskCanceledException) { }

            _cts.Dispose();
            _cts = null;
            _workerTask = null;
        }

        private async Task RunAsync(CancellationToken token)
        {
            OnLog?.Invoke("Checking for Updates ...");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await CheckOnceAsync(token);
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke($"Error: {ex.Message}");
                }

                await Task.Delay(_interval, token);
            }

            OnLog?.Invoke("Updater stopped.");
        }

        private async Task CheckOnceAsync(CancellationToken token)
        {
            if (!await _lock.WaitAsync(0, token))
                return; // skip if already running

            try
            {
                if (!ApplicationDeployment.IsNetworkDeployed)
                    return;

                if (!File.Exists(_networkFile))
                {
                    OnLog?.Invoke("No network file.");
                    return;
                }

                var versionLine = File.ReadAllLines(_networkFile)
                    .FirstOrDefault(x => x.StartsWith("Version=", StringComparison.OrdinalIgnoreCase));

                if (versionLine == null)
                    return;

                string versionText = versionLine.Replace("Version=", "").Trim();

                if (!Version.TryParse(versionText, out Version networkVersion))
                {
                    OnLog?.Invoke("Invalid version format.");
                    return;
                }

                var deployment = ApplicationDeployment.CurrentDeployment;
                Version currentVersion = deployment.CurrentVersion;

                OnLog?.Invoke($"Current: {currentVersion} | Network: {networkVersion}");

                // 🔴 HARD GATE
                if (networkVersion <= currentVersion)
                    return;

                // 🟢 Only now check ClickOnce
                var info = await Task.Run(() => deployment.CheckForDetailedUpdate());

                if (!info.UpdateAvailable)
                    return;

                if (info.AvailableVersion <= currentVersion)
                    return;

                OnUpdateStarted?.Invoke(currentVersion, info.AvailableVersion);

                OnLog?.Invoke($"Updating {currentVersion} → {info.AvailableVersion}");

                deployment.Update();

                // restart MUST be on UI thread
                RestartApplicationSafe();
            }
            finally
            {
                _lock.Release();
            }
        }

        private void RestartApplicationSafe()
        {
            if (System.Windows.Forms.Application.OpenForms.Count > 0)
            {
                var form = System.Windows.Forms.Application.OpenForms[0];

                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() =>
                    {
                        System.Windows.Forms.Application.Restart();
                    }));
                }
                else
                {
                    System.Windows.Forms.Application.Restart();
                }
            }
            else
            {
                System.Windows.Forms.Application.Restart();
            }
        }

        public void Dispose()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _lock?.Dispose();
        }
    }
}
