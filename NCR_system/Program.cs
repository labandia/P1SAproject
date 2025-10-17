﻿using Microsoft.Extensions.DependencyInjection;
using NCR_system.Interface;
using NCR_system.Repository;
using NCR_system.View.AddForms;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system
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
            services.AddSingleton<ICustomerComplaint, CustomerRepository>();
            services.AddSingleton<IShipRejected, RejectShipRepository>();
            services.AddSingleton<IInprocess, InprocessRepository>();
            services.AddSingleton<INCR, NCR_Repository>();

            // Import Modules Usercontrols
            services.AddSingleton<Mainpage>();
            services.AddSingleton<AddCustomerComplaint>();
            services.AddSingleton<Form1>();

            services.AddTransient<Customer_Complaint_user>();
            services.AddTransient<Inprocess_control>();
            services.AddTransient<NCR_control>();
            services.AddTransient<Rejected>();
            services.AddTransient<ShipRejected>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Mainpage>();
            Application.Run(mainForm);
        }
    }
}
