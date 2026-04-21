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
            // Services (OK as Singleton)
            services.AddSingleton<ITraceable, TraceableService>();
            services.AddSingleton<ISubassy, ISubassyServices>();
            services.AddSingleton<ISummary, SummaryServices>();

            // Forms (should be Transient)
            services.AddTransient<Form1>();
            services.AddTransient<FanTraceabilityAutoSearch>();
            services.AddTransient<TraceableHistory>();
            services.AddTransient<AddPCBShop>();

            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
    }
}
