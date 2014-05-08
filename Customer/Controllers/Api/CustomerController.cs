using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.WebHost;
using Customer.WcfService;
using StructureMap;

namespace Customer.Controllers.Api
{

    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class Customers
    {
        public Customers()
        {
            CustomerList = new List<Customer>();
        }

        public List<Customer> CustomerList { get; set; }
    }

    public class CustomerController : ApiController
    {
        private readonly IBusiness _business = ObjectFactory.GetInstance<IBusiness>();

       // GET /Api/Customer/SearchCustomer
        [System.Web.Http.AcceptVerbs("GET")]
        public Customers SearchCustomer(string searchKey)
        {
            var response = new Customers();

            var search = _business.Search(searchKey);
            response.CustomerList.AddRange(from cust in search.Customers select new Customer { Id = cust.Id, Name = cust.Name });
            return response;
        }

       // GET /Api/Customer/GetCustomer
        public Customer GetCustomer([FromUri]Guid customerId)
        {
           var searched = _business.Search(customerId);
           return new Customer { Id = searched.Customers[0].Id, Name = searched.Customers[0].Name };
        }

        // POST /Api/Customer/SaveCustomer
        public Customer SaveCustomer([FromBody]Customer customer)
        {
            var dataCustomer = new DataAccess.Customer
            {
                Id = customer.Id,
                Name = customer.Name
            };
            var cust = _business.Save(dataCustomer);

            return new Customer { Id = cust.Id, Name = cust.Name }; ;
        }

        // GET /Api/Customer/DeleteCustomer
        [System.Web.Http.AcceptVerbs("GET")]
        public bool DeleteCustomer([FromUri]Guid customerIdToDelete)
        {
            return _business.Delete(customerIdToDelete);
        }
    }
}