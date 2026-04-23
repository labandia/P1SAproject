using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FanTraceableSystem.Services
{
    public static class ClickOnceUpdateManager
    {
        // 🔴 NETWORK VERSION FILE
        private static readonly string _networkFile =
            @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\SystemVersion\SubAssyVersion.txt";

        // 🟢 LOCAL FILE
        private static readonly string _localFile =
            Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "SubAssy",
                "last_version.txt");

        public static async Task CheckAndUpdateAsync(Action<string> notify = null)
        {
            await Task.Delay(3000);

            try
            {
                var config = ReadConfig();
                var current = GetCurrentVersion();
                var local = GetLocalVersion();
                var now = DateTime.Now;

                Debug.WriteLine("LOCAL : " + local);
                Debug.WriteLine("Current  : " + current);
                Debug.WriteLine("NOw  : " + now);

                Debug.WriteLine("NextUpdated  : " + config.NextUpdated.Value);

                // ⏱ TIME GATE (NextUpdated)
                if (config.NextUpdated.HasValue && now < config.NextUpdated.Value)
                    return;

                // 🔴 HARD BLOCK (MinRequiredVersion)
                if (config.MinRequiredVersion != null && current < config.MinRequiredVersion)
                {
                    notify?.Invoke("Version too old. Updating...");
                    await PerformUpdate(notify);
                    SaveLocalVersion(config.Version?.ToString());
                    return;
                }

                // 🟠 FORCE UPDATE
                if (config.ForceUpdate)
                {
                    notify?.Invoke("Forced update triggered...");
                    await PerformUpdate(notify);
                    SaveLocalVersion(config.Version?.ToString());
                    return;
                }

                // 🟢 NORMAL UPDATE
                if (config.Version != null && current < config.Version)
                {
                    notify?.Invoke($"Update available: {config.Version}");
                    await PerformUpdate(notify);
                    SaveLocalVersion(config.Version.ToString());
                }
            }
            catch (Exception ex)
            {
                notify?.Invoke($"Update error: {ex.Message}");
            }
        }

        // =========================
        // 🟢 CONFIG PARSER
        // =========================
        private class UpdateConfig
        {
            public Version Version;
            public Version MinRequiredVersion;
            public bool ForceUpdate;
            public DateTime? NextUpdated;
        }

        private static UpdateConfig ReadConfig()
        {
            var config = new UpdateConfig();

            if (!File.Exists(_networkFile))
                return config;

            foreach (var line in File.ReadAllLines(_networkFile))
            {
                if (line.StartsWith("Version="))
                    Version.TryParse(line.Replace("Version=", "").Trim(), out config.Version);

                if (line.StartsWith("MinRequiredVersion="))
                    Version.TryParse(line.Replace("MinRequiredVersion=", "").Trim(), out config.MinRequiredVersion);

                if (line.StartsWith("ForceUpdate="))
                    bool.TryParse(line.Replace("ForceUpdate=", "").Trim(), out config.ForceUpdate);

                if (line.StartsWith("NextUpdated="))
                {
                    if (DateTime.TryParse(line.Replace("NextUpdated=", "").Trim(),
                        null,
                        System.Globalization.DateTimeStyles.RoundtripKind,
                        out DateTime dt))
                    {
                        config.NextUpdated = dt;
                    }
                }
            }

            return config;
        }

        // =========================
        // 🟢 CURRENT VERSION
        // =========================
        private static Version GetCurrentVersion()
        {
       
            if (ApplicationDeployment.IsNetworkDeployed)
                return ApplicationDeployment.CurrentDeployment.CurrentVersion;

            return new Version(0, 0, 0, 0);
        }

        // =========================
        // 🟢 LOCAL VERSION
        // =========================
        private static string GetLocalVersion()
        {
            try
            {
                var dir = Path.GetDirectoryName(_localFile);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (!File.Exists(_localFile))
                {
                    var current = GetCurrentVersion().ToString();
                    File.WriteAllText(_localFile, current);
                    return current;
                }

                return File.ReadAllText(_localFile).Trim();
            }
            catch
            {
                return null;
            }
        }

        private static void SaveLocalVersion(string version)
        {
            try
            {
                var dir = Path.GetDirectoryName(_localFile);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                File.WriteAllText(_localFile, version ?? "0.0.0.0");
            }
            catch { }
        }

        // =========================
        // 🟢 CLICKONCE UPDATE
        // =========================
        private static async Task PerformUpdate(Action<string> notify)
        {
            if (!ApplicationDeployment.IsNetworkDeployed)
                return;

            try
            {
                var deployment = ApplicationDeployment.CurrentDeployment;

                var info = await Task.Run(() => deployment.CheckForDetailedUpdate());

                if (info.UpdateAvailable)
                {
                    Version current = deployment.CurrentVersion;
                    Version latest = info.AvailableVersion;

                    notify?.Invoke($"Updating...\n{current} → {latest}");

                    deployment.Update();

                    Application.OpenForms[0]?.Invoke(new Action(() =>
                    {
                        Application.Restart();
                    }));
                }
            }
            catch (Exception ex)
            {
                notify?.Invoke($"Update failed: {ex.Message}");
            }
        }
    }

}
