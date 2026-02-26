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
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dbFolder = Path.Combine(documentsPath, "SariSariStore");

            if (!Directory.Exists(dbFolder))
                Directory.CreateDirectory(dbFolder);

            // This makes |DataDirectory| point to Documents\SariSariStore
            AppDomain.CurrentDomain.SetData("DataDirectory", dbFolder);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SariSariStoreLogin());
        }
    }
}
