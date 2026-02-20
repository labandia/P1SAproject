using System;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Drawing;

namespace NCR_system.Utilities
{
    public static class UploadServices
    {
        public static string folderPath = ConfigurationManager.AppSettings["NonConformityImagePath"];


        public static async Task<string> SaveImageFolder(string strpath)
        {
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

    }
}
