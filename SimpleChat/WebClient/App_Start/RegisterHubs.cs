using System.Web.Routing;
using WebClient.App_Start;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RegisterHubs), "Start")]

namespace WebClient.App_Start
{
    public static class RegisterHubs
    {
        public static void Start()
        {
            // Register the default hubs route: ~/signalr
            RouteTable.Routes.MapHubs();
        }
    }
}
