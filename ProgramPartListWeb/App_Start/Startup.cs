using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(ProgramPartListWeb.App_Start.Startup))]

namespace ProgramPartListWeb.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.Diagnostics.Debug.WriteLine("🚀 OWIN Startup running");
            app.MapSignalR();
        }
    }
}
