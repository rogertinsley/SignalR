using Microsoft.AspNet.SignalR;

namespace Datagrid.Models
{
    public class CustomerHub : Hub
    {
        public void Send(Customer customer)
        {
            Clients.Others.broadcastMessage(customer);
        }
    }
}