using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(PMACS_V2.Startup))]

namespace PMACS_V2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}