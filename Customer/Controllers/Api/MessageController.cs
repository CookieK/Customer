using System.Web.Http;
using System.Web.Mvc;
using Customer.SignalR;
using Customer.WcfService;
using Microsoft.AspNet.SignalR;
using StructureMap;

namespace Customer.Controllers.Api
{

   public class CustomerMessage
    {
        public Customer From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }

   public class MessageController : ApiController
    {
        private readonly IBusiness _business = ObjectFactory.GetInstance<IBusiness>();

       // POST /Api/Message/SaveMessage
        public CustomerMessage SaveMessage([FromBody]CustomerMessage message)
        {
            var dataMessage = new DataAccess.CustomerMessage
            {
                From = new DataAccess.Customer
                {
                    Id = message.From.Id,
                    Name = message.From.Name
                },
                To = message.To,
                Text = message.Text
            };
            _business.SaveMessage(dataMessage);

            return message;
        }

        [System.Web.Http.AcceptVerbs("PUT")]
        public void ReceiveMessage([FromBody]CustomerMessage message)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CustomerHub>();
            context.Clients.All.receiveMessage(message);
        }
    }
}