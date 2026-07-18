using IssuanceSystem.Interface;
using IssuanceSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IssuanceSystem
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
            services.AddSingleton<IIsuanceRespository, IsuanceRepository>();

            // Forms (should be Transient)
            services.AddTransient<Form1>();

            ServiceProvider = services.BuildServiceProvider();


            Application.Run(ServiceProvider.GetRequiredService<Form1>());
        }
    }
}
