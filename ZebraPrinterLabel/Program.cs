using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace ZebraPrinterLabel
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
            services.AddSingleton<IMasterlist, MasterlistRepository>();

            services.AddSingleton<ZebraPrinter>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<ZebraPrinter>();
            Application.Run(mainForm);
        }
    }
}
