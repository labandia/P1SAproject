using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FanTraceableSystem.Interface;
using FanTraceableSystem.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FanTraceableSystem
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
            services.AddSingleton<ITraceable, TraceableService>();
            services.AddSingleton<FanTraceabilityAutoSearch>();
            services.AddSingleton<AddPCBShop>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<FanTraceabilityAutoSearch>();
            Application.Run(mainForm);
        }
    }
}
