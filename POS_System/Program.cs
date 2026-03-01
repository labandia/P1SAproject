using POS_System.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

            // ======= Ensure Data Folder Exists =======
            string dataFolder = Path.Combine(Application.StartupPath, "Data");
            if (!Directory.Exists(dataFolder))
            {
                Directory.CreateDirectory(dataFolder);
            }

            // ======= Set DataDirectory for connection strings =======
            AppDomain.CurrentDomain.SetData("DataDirectory", dataFolder);

           
            Application.Run(new Flashscreen());
        }
    }
}
