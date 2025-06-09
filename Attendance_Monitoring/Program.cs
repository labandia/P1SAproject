using Attendance_Monitoring.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Usercontrols;

namespace Attendance_Monitoring
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
            services.AddSingleton<IEmployee, EmployeeRespository>();
            services.AddSingleton<ICRmonitor, CRMonitoringRespository>();
            services.AddSingleton<IAttendance, AttendanceRepository>();

            services.AddSingleton<Selection>();
            services.AddSingleton<Mainpage>();
            services.AddSingleton<Summary>();
            services.AddSingleton<CRMainpage>();
            services.AddSingleton<CRMonitoringPage>();
            services.AddSingleton<Attendance>();
            services.AddSingleton<EmployeeManage>();
            services.AddSingleton<UploadsData>();

            services.AddSingleton<Selection>();
            services.AddTransient<AttendancePage>();
            services.AddTransient<AttendanceSelection>();
            services.AddTransient<CRSelection>();
            services.AddTransient<CRMonitoringPage>();
            services.AddTransient<EmployeeManagement>();



            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<Selection>();
            Application.Run(mainForm);
        }
    }
}
