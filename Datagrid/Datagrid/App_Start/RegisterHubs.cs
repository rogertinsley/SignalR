using System.Web.Routing;
using Datagrid.App_Start;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(RegisterHubs), "Start")]

namespace Datagrid.App_Start
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
