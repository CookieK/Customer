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

    public class CustomerMessage
    {
        public Customer From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
    }

    public class MessageController : ApiController
    {
        private readonly IBusiness _business = ObjectFactory.GetInstance<IBusiness>();

        [System.Web.Http.AcceptVerbs("PUT")]
        public IEnumerable<CustomerMessage> ListMessages([FromBody]Customer customer)
        {
            var dataMessage = new DataAccess.CustomerMessage { From = new DataAccess.Customer { Id = customer.Id, Name = customer.Name } };

            var response = _business.ListMessages(dataMessage.From);

            return from r in response
                   select new CustomerMessage
                   {
                       From = new Customer { Id = r.From.Id, Name = r.From.Name },
                       To = r.To,
                       Text = r.Text
                   };
        }

        // POST /Api/Message/SaveMessage
        public void SaveMessage([FromBody]CustomerMessage message)
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
        }
    }
}