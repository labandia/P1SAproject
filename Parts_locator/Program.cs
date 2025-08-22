using Microsoft.Extensions.DependencyInjection;
using Parts_locator.Interface;
using Parts_locator.Modals;
using Parts_locator.Repository;
using Parts_locator.View.Moldingbush;
using Parts_locator.View.Moldingbush.Maincontent;
using Parts_locator.View.Moldingbush.Modules;
using System;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using Parts_locator.View.Rotor;

namespace Parts_locator
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main()
        {

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);

            // Initialize your repositories.
            //IProductRepository ip = new SQLMainlayoutRespository();


            // Initialize your views.
            //IMainlayoutView mainview = new Mainlayout();

            // Set presenters to the views (if necessary).
            // MainlayoutPresentor mainpresentor = new MainlayoutPresentor(mainview, ip);


            // Set presenters to the views (if necessary).

            string localIP = GetLocalIPv4();
            if (localIP == null)
            {
                MessageBox.Show("Cannot determine local IP address. Application will exit.");
                return;
            }


            var octets = localIP.Split('.');
            if (octets.Length != 4)
            {
                MessageBox.Show($"Invalid IP address: {localIP}");
                return;
            }

            string department = null;

            switch (octets[2])
            {
                case "7":
                    department = "Molding";
                    break;
                case "2":
                    department = "Press";
                    break;
                case "1":
                    department = "Rotor";
                    break;
                case "4":
                    department = "Winding";
                    break;
                case "5":
                    department = "Circuit";
                    break;
                default:
                    department = null;
                    break;
            }

            if (department == null)
            {
                MessageBox.Show($"This program cannot run on this network segment: {localIP}");
                return;
            }
            //Application.Run(new Startup());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            services.AddSingleton<IRawMats, RawMatsRepository>();

            services.AddSingleton<BushMain>();
            services.AddSingleton<Startup>();
            services.AddSingleton<RawMaterialProductDetails>();
            services.AddSingleton<MoldShoporder_IN_dialog>();
            services.AddSingleton<BushOpentransaction>();
            services.AddSingleton<RawMaterialOpentraction>();
            services.AddSingleton<Bushlocation>();
            services.AddSingleton<Mainlayout>();
            services.AddSingleton<BushMasterlist>();
            services.AddSingleton<BushSummary_in>();
            services.AddSingleton<BushSummary_out>();
            services.AddSingleton<BushPartDetails>();
            services.AddSingleton<EditMasterlist>();

 

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Startup>();
            Application.Run(mainForm);
            //// Dynamically choose which form to run
            //Form mainForm = null;
            //if (department == "Molding")
            //    mainForm = ServiceProvider.GetRequiredService<Startup>();
            //else if (department == "Press")
            //    mainForm = ServiceProvider.GetRequiredService<Startup>();
            //else if (department == "Rotor")
            //    mainForm = ServiceProvider.GetRequiredService<RotorPartsLocator>();
            //else if (department == "Winding")
            //    mainForm = ServiceProvider.GetRequiredService<Startup>();
            //else if (department == "Circuit")
            //    mainForm = ServiceProvider.GetRequiredService<Startup>();

            //MessageBox.Show($"Running program for department: {department}");
            //Application.Run(mainForm);



        }

        private static string GetLocalIPv4()
        {
            foreach (var ni in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (ni.AddressFamily == AddressFamily.InterNetwork)
                    return ni.ToString();
            }
            return null;
        }
    }   

    
}   
