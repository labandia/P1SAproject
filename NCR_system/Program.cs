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

            var services = new ServiceCollection()
            .AddMemoryCache()
            .AddScoped<ICustomerComplaint, CustomerRepository>()
            .AddScoped<IShipRejected, RejectShipRepository>()
            .AddScoped<IInprocess, InprocessRepository>()
            .AddScoped<INCR, NCR_Repository>()
            .AddTransient<P1SA_NonComformity>()
            .AddTransient<Mainpage>()
            .AddTransient<AddCustomerComplaint>()
            .AddTransient<AddShipment>()
            .AddTransient<EditCC_External>()
            .AddTransient<EditShipments>()
            .AddTransient<EditRejected>()
            .AddTransient<Customer_Complaint_user>()
            .AddTransient<Inprocess_control>()
            .AddTransient<Dashboard>()
            .AddTransient<NCR_control>()
            .AddTransient<Rejected>()
            .AddTransient<ShipRejected>();

            ServiceProvider = services.BuildServiceProvider(new ServiceProviderOptions
            {
                ValidateOnBuild = false,
                ValidateScopes = false
            });

            using (var scope = ServiceProvider.CreateScope())
            {
                var mainForm = scope.ServiceProvider.GetRequiredService<P1SA_NonComformity>();
                Application.Run(mainForm);
            }
            
        }
    }
}
