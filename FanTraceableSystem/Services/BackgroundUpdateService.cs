using FanTraceableSystem.Interface;
using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FanTraceableSystem.Services
{
    public class BackgroundUpdateService : IDisposable
    {
        private static BackgroundUpdateService _instance;

        public static bool IsInitialized => _instance != null;


        public event Action<string> OnLog;
        public event Action<Version, Version> OnUpdateStarted;
        public event Action OnUpdateCompleted;


        public static void Initialize(IUpdateRepository repository)
        {
            if (_instance != null)
                return;

            _instance = new BackgroundUpdateService(repository);
        }


        public static BackgroundUpdateService Instance =>
               _instance ?? throw new Exception("BackgroundUpdateService not initialized.");

        private readonly IUpdateRepository _repository;

        private readonly TimeSpan _interval = TimeSpan.FromSeconds(5);

        private CancellationTokenSource _cts;
        private Task _workerTask;
        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);


        private BackgroundUpdateService(IUpdateRepository repository)
        {
            _repository = repository;
        }


        // =========================
        // START
        // =========================
        public void Start()
        {
            if (_workerTask != null)
                return;

            _cts = new CancellationTokenSource();
            _workerTask = Task.Run(() => RunAsync(_cts.Token));
        }

        // =========================
        // STOP
        // =========================
        public async Task StopAsync()
        {
            if (_cts == null)
                return;

            _cts.Cancel();

            try
            {
                await _workerTask;
            }
            catch { }

            _cts.Dispose();
            _cts = null;
            _workerTask = null;
        }

        // =========================
        // LOOP
        // =========================
        private async Task RunAsync(CancellationToken token)
        {
            OnLog?.Invoke("Background updater started.");

            while (!token.IsCancellationRequested)
            {
                try
                {
                    await CheckForUpdatesAsync(token);
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke($"Update Error: {ex.Message}");
                }

                await Task.Delay(_interval, token);
            }
        }

        // =========================
        // MAIN CHECK
        // =========================
        private async Task CheckForUpdatesAsync(CancellationToken token)
        {
            if (!await _lock.WaitAsync(0, token))
                return;

            try
            {
                if (!ApplicationDeployment.IsNetworkDeployed)
                {
                    OnLog?.Invoke("Not ClickOnce deployed.");
                    return;
                }

                // DB VERSION
                var dbVersion = await _repository.GetLatestVersionAsync("Sub Assy Traceablity Auto Search System");

                if (dbVersion == null)
                {
                    OnLog?.Invoke("No version found in DB.");
                    return;
                }

                if (!Version.TryParse(dbVersion.VersionNumber, out Version databaseVersion))
                {
                    OnLog?.Invoke("Invalid DB version format.");
                    return;
                }

                var deployment = ApplicationDeployment.CurrentDeployment;
                Version currentVersion = deployment.CurrentVersion;

                OnLog?.Invoke($"Current: {currentVersion}");
                OnLog?.Invoke($"Database: {databaseVersion}");

                // =========================
                // MIN REQUIRED VERSION CHECK
                // =========================
                if (!string.IsNullOrWhiteSpace(dbVersion.MinRequiredVersion) &&
                    Version.TryParse(dbVersion.MinRequiredVersion, out Version minRequired))
                {
                    if (currentVersion < minRequired)
                    {
                        OnLog?.Invoke($"Blocked. Minimum required: {minRequired}");
                    }
                }

                // =========================
                // HARD GATE
                // =========================
                bool shouldUpdate =
                    dbVersion.ForceUpdate ||
                    databaseVersion > currentVersion;

                if (!shouldUpdate)
                {
                    OnLog?.Invoke("No update needed.");
                    return;
                }

                // ONLY NOW ask ClickOnce
                var info = await Task.Run(() => deployment.CheckForDetailedUpdate());

                if (!info.UpdateAvailable)
                {
                    OnLog?.Invoke("No ClickOnce update available.");
                    return;
                }

                if (!dbVersion.ForceUpdate &&
                    info.AvailableVersion <= currentVersion)
                {
                    OnLog?.Invoke("ClickOnce version is not newer.");
                    return;
                }

                OnUpdateStarted?.Invoke(currentVersion, info.AvailableVersion);
                OnLog?.Invoke($"Updating {currentVersion} -> {info.AvailableVersion}");

                deployment.Update();

                OnUpdateCompleted?.Invoke();

                RestartApplication();
            }
            finally
            {
                _lock.Release();
            }
        }


        // =========================
        // SAFE RESTART
        // =========================
        private void RestartApplication()
        {
            if (Application.OpenForms.Count > 0)
            {
                var form = Application.OpenForms[0];

                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() =>
                    {
                        Application.Restart();
                    }));
                }
                else
                {
                    Application.Restart();
                }
            }
            else
            {
                Application.Restart();
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
