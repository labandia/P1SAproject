using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssuanceSystem.Services
{
    public static class ClickOnceUpdateManager
    {
        // 🔴 NETWORK VERSION FILE
        private static readonly string _networkFile =
            @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\SystemVersion\SubAssyVersion.txt";

        public static async Task CheckAndUpdateAsync(Action<string> notify = null)
        {
            await Task.Delay(2000); // allow app to fully load

            try
            {
                // ❌ Not ClickOnce deployed → skip
                if (!ApplicationDeployment.IsNetworkDeployed)
                {
                    notify?.Invoke("Not a ClickOnce app.");
                    return;
                }

                // ❌ Network file missing → skip
                if (!File.Exists(_networkFile))
                {
                    notify?.Invoke("Update file not found.");
                    return;
                }

                // ✅ Read Version from file
                var versionLine = File.ReadAllLines(_networkFile)
                    .FirstOrDefault(x => x.StartsWith("Version=", StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrWhiteSpace(versionLine))
                {
                    notify?.Invoke("Version not found in config.");
                    return;
                }

                string versionText = versionLine.Replace("Version=", "").Trim();

                if (!Version.TryParse(versionText, out Version networkVersion))
                {
                    notify?.Invoke($"Invalid version format: {versionText}");
                    return;
                }

                // ✅ Get installed ClickOnce version
                Version currentVersion = ApplicationDeployment
                    .CurrentDeployment
                    .CurrentVersion;

                // 🔍 DEBUG OUTPUT
                notify?.Invoke($"Current Version: {currentVersion}");
                notify?.Invoke($"Network Version: {networkVersion}");

                // ============================================
                // 🔴 HARD GATE (THIS IS THE FIX)
                // ============================================
                if (networkVersion <= currentVersion)
                {
                    notify?.Invoke("⛔ Update BLOCKED (network version is not higher)");
                   // MessageBox.Show("No update available. Current version is up to date.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // 🚫 STOP → NO ClickOnce check
                }

                // ============================================
                // 🟢 ONLY NOW we allow ClickOnce to check
                // ============================================
                var deployment = ApplicationDeployment.CurrentDeployment;

                var info = await Task.Run(() => deployment.CheckForDetailedUpdate());

                if (!info.UpdateAvailable)
                {
                    notify?.Invoke("No update available from ClickOnce.");
                    return;
                }

                // 🔍 Extra safety (prevents weird downgrade or mismatch)
                if (info.AvailableVersion <= currentVersion)
                {
                    notify?.Invoke("⛔ ClickOnce version is not newer. Skipping.");
                    //MessageBox.Show("1111 No update available. Current version is up to date.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                notify?.Invoke($"🚀 Updating {currentVersion} → {info.AvailableVersion}");

                // 🔄 Perform update
                deployment.Update();

                //.Show("UPDATING TO NEW ONE.", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔁 Restart app safely on UI thread
                Application.OpenForms[0]?.Invoke(new Action(() =>
                {
                    Application.Restart();
                }));
            }
            catch (Exception ex)
            {
                notify?.Invoke($"❌ Update failed: {ex.Message}");
            }
        }
    }

}
