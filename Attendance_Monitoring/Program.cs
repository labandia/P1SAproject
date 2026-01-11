using Attendance_Monitoring.View;
using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Attendance_Monitoring.Repositories;
using Attendance_Monitoring.Models;
using Attendance_Monitoring.Usercontrols;
using Attendance_Monitoring.View.V2;
using Attendance_Monitoring.Interfaces;

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
            //services.AddSingleton<IEmployee, EmployeeRespository>();
            services.AddSingleton<ICRmonitor, CRMonitoringRespository>();
            services.AddSingleton<IAttendance, AttendanceRepository>();
            services.AddSingleton<IAttendanceV2, AttendanceV2Repository>();

            services.AddSingleton<IAttendanceMonitor, AttendanceMonitorRepository>();

            services.AddSingleton<IEmployee, EmployeeRespositoryV2>();

            services.AddSingleton<Selection>();
            services.AddSingleton<Mainpage>();
            services.AddSingleton<Summary>();
            services.AddSingleton<SummaryV2>();
            services.AddSingleton<CRMainpage>();
            services.AddSingleton<CRMonitoringPage>();
            services.AddSingleton<Attendance>();
            services.AddSingleton<EmployeeManage>();
            services.AddSingleton<UploadsData>();

            services.AddTransient<AttendancePage>();
            services.AddTransient<AttendanceSelection>();
            services.AddTransient<CRSelection>();
            services.AddTransient<EmployeeManagement>();
            services.AddSingleton<MainLayout>();
            services.AddTransient<AttendanceMain>();

            ServiceProvider = services.BuildServiceProvider();
            var mainForm = ServiceProvider.GetRequiredService<AttendanceMain>();
            Application.Run(mainForm);
        }
    }
}
