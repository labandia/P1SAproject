using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using NCR_system.Interface;
using NCR_system.Repository;
using NCR_system.View.AddForms;
using NCR_system.View.EditForms;
using NCR_system.View.Module;
using System;
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

            services.AddMemoryCache();

            services.AddSingleton<ICustomerComplaint, CustomerRepository>();
            services.AddSingleton<IShipRejected, RejectShipRepository>();
            services.AddSingleton<IInprocess, InprocessRepository>();
            services.AddSingleton<INCR, NCR_Repository>();

            // Forms → Transient
            services.AddTransient<P1SA_NonComformity>();
            services.AddTransient<Mainpage>();
            services.AddTransient<NonConformity>();
            services.AddTransient<AddCustomerComplaint>();
            services.AddTransient<AddShipment>();

            services.AddTransient<EditCC_External>();
            services.AddTransient<EditShipments>();
            services.AddTransient<EditRejected>();

            // UserControls → Transient
            services.AddTransient<Customer_Complaint_user>();
            services.AddTransient<Inprocess_control>();
            services.AddTransient<Dashboard>();
            services.AddTransient<NCR_control>();
            services.AddTransient<Rejected>();
            services.AddTransient<ShipRejected>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<P1SA_NonComformity>();
            Application.Run(mainForm);
        }
    }
}
