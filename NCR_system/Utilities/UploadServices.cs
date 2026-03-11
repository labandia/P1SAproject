using System;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NCR_system.Utilities
{
    public static class UploadServices
    {
        public static string folderPath = ConfigurationManager.AppSettings["NonConformityImagePath"];
        public static string networkFolder = $@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\Templates\NCR";

        public static async Task<string> SaveImageFolder(string strpath)
        {
            // If no file selected, return empty string
            if (string.IsNullOrWhiteSpace(strpath) || !File.Exists(strpath))
                return string.Empty;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string filename = Guid.NewGuid().ToString() + Path.GetExtension(strpath);
            string des = Path.Combine(folderPath, filename);

            await Task.Run(() =>
            {
                File.Copy(strpath, des, true);
            });

            return des;
        }


        /// CReates a Thumbnail Image
        public static Image CreateThumbnail(string imagePath, int width = 100, int height = 80)
        {
            using (var fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            using (var img = Image.FromStream(fs))
            {
                Bitmap thumb = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(thumb))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(img, 0, 0, width, height);
                }
                return thumb;
            }
        }


        public static void DownloadFiles(int process)
        {
            try
            {
                string fileName = "";

                switch(process)
                {
                    case 1:
                        fileName = "CC_template.xlsx";
                        break;
                    case 2:
                        fileName = "SR_template.xlsx";
                        break;
                    case 3:
                        fileName = "R_template.xlsx";
                        break;
                }

                string sourceFile = Path.Combine(networkFolder, fileName);

                if (!File.Exists(sourceFile))
                {
                    MessageBox.Show("Template file not found.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using(SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = fileName;
                    sfd.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(sourceFile, sfd.FileName, true);
                        MessageBox.Show("Template downloaded successfully.", "Success",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}",
                      "Download Failed",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
            }
        }

    }
}
