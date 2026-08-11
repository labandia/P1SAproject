using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(PMACS_V2.App_Start.Startup))]

namespace PMACS_V2.App_Start
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