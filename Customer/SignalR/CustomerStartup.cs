using Customer.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CustomerStartup))]

namespace Customer.SignalR
{
    public class CustomerStartup
    {
        public void Configuration(IAppBuilder customerApp)
        {
            customerApp.MapSignalR();
        }
    }
}
