using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Customer.SignalR;
using Customer.WcfService;
using Microsoft.AspNet.SignalR;
using StructureMap;

namespace Customer.Controllers.Api
{
    public class HubController : ApiController
    {
       [System.Web.Http.AcceptVerbs("PUT")]
        public void ReceiveMessage([FromBody]CustomerMessage message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.receiveMessage(message);
        }
    }
}