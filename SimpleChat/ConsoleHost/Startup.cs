using Microsoft.AspNet.SignalR;
using Owin;

namespace ConsoleHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapHubs(new HubConfiguration
                    {
                         EnableCrossDomain = true
                    }
            );
        }
    }
}
