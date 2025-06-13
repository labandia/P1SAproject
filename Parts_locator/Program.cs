using Parts_locator.DataAccess;
using Parts_locator.Interface;
using Parts_locator.Modals;
using Parts_locator.Models;
using Parts_locator.Repository;
using Parts_locator.View.Moldingbush;
using Parts_locator.View.Moldingbush.Maincontent;
using Parts_locator.View.Moldingbush.Modules;
using Parts_locator.View.Rotor;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Parts_locator
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static PrivateFontCollection privateFonts = new PrivateFontCollection();

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
            


            //Application.Run(new Startup());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var services = new ServiceCollection();
            services.AddSingleton<IRawMats, RawMatsRepository>();

            services.AddSingleton<BushMain>();
            services.AddSingleton<RawMaterialProductDetails>();
            services.AddSingleton<Bushlocation>();
            services.AddSingleton<BushMasterlist>();
            services.AddSingleton<BushSummary_in>();
            services.AddSingleton<BushSummary_out>();
            services.AddSingleton<BushPartDetails>();
            services.AddSingleton<EditMasterlist>();

 

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Startup>();
            Application.Run(mainForm);



        }
    }   

    
}   
