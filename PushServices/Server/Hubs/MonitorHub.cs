using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Server.Hubs
{
    [HubName("monitor")]
    public class MonitorHub : Hub
    {
    }
}
