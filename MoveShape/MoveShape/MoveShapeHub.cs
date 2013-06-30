using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace MoveShape
{
    [HubName("moveShape")]
    public class MoveShapeHub : Hub
    {
        public void MoveShape(int x, int y)
        {
            Clients.Others.shapeMoved(x, y);
        }
    }
}