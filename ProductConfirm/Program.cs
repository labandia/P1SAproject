using ProductConfirm.Data;
using Microsoft.Extensions.DependencyInjection;
using ProductConfirm.Modules;
using System;
using System.Windows.Forms;
using ProductConfirm.DataAccess;
using ProductConfirm.Models;

namespace ProductConfirm
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
            services.AddSingleton<IProductRepositoryV2, ProductRepositoryV2>();
            services.AddSingleton<IUsers, UserRespository>();

            services.AddSingleton<Loginpage>();
            services.AddSingleton<Mainpage>();
            services.AddTransient<UIShoporder>();
            services.AddTransient<Summary>();
            services.AddTransient<Masterlistpage>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Mainpage>();
            Application.Run(mainForm);
        }

       

      
    }
}
