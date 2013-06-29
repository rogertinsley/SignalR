using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ConsoleHost
{
    [HubName("chat")]
    public class ChatHub : Hub
    {
        public void SendMessage(string message)
        {
            var msg = String.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.All.newMessage(msg);
        }

        public void JoinRoom(string room)
        {
            Groups.Add(Context.ConnectionId, room);
        }

        public void SendMessageToRoom(string room, string message)
        {
            var msg = String.Format("{0}: {1}", Context.ConnectionId, message);
            Clients.Group(room).newMessage(msg);
        }

        public void SendMessageData(SendData data)
        {
            Clients.All.newData(data);
        }
    }

    public class SendData
    {
        public int Id { get; set; }
        public string Data { get; set; }
    }
}