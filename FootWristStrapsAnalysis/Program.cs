using FootWristStrapsAnalysis.Interface;
using FootWristStrapsAnalysis.Services;
using Microsoft.Extensions.DependencyInjection;
using OfficeOpenXml;
using System;
using System.Windows.Forms;



namespace FootWristStrapsAnalysis
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // obsolete


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            services.AddSingleton<IFootWrist>(sp => { 
                var service = new FootWristServices(); 
                return new CachedFootWristServices(service); 
            });

            // 🔹 Register Forms
            services.AddTransient<Startup>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Startup>();
            Application.Run(mainForm);
        }
    }
}
