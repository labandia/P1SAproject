using Microsoft.Owin;
using Owin;


[assembly: OwinStartup(typeof(SanyoDenki.App_Start.Startup))]

namespace SanyoDenki.App_Start
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