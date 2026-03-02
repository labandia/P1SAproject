using System;
using System.IO;
using System.Windows.Forms;

namespace POS_System
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ======= Set Database Folder Inside Documents =======
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string appFolder = Path.Combine(documentsPath, "SariSariStore");

            // Create folder if it doesn't exist
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }

            // ======= Set DataDirectory for connection strings =======
            AppDomain.CurrentDomain.SetData("DataDirectory", appFolder);
            Application.Run(new Flashscreen());
        }
    }
}
