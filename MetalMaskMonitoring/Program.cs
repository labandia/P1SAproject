using System;
using System.Windows.Forms;
using MetalMaskMonitoring.Interface;
using MetalMaskMonitoring.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MetalMaskMonitoring
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            services.AddSingleton<IMaskMasterlist, MasterlistServices>();
            // 🔹 Register Forms
            services.AddTransient<Masterlist>();
            services.AddTransient<MetalMaskMonitoring>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<MetalMaskMonitoring>();
            Application.Run(mainForm);
        }
    }
}
