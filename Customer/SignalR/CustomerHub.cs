using Microsoft.AspNet.SignalR;

namespace Customer.SignalR
{
    public class CustomerHub : Hub
    {
        public void Send(Controllers.Api.CustomerMessage message)
        {
            Clients.All.receiveMessage(message);
        }
    }
}