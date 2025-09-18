using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(ProgramPartListWeb.Startup))]

namespace ProgramPartListWeb
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            System.Diagnostics.Debug.WriteLine("🚀 OWIN Startup running");
            app.MapSignalR();   // 🚀 registers /signalr/hubs and negotiate
        }
    }
}
