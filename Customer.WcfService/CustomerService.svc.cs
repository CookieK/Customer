using System;
using StructureMap;

namespace Customer.WcfService
{
    public class CustomerService : ICustomerService
    {
        private readonly IBusiness _business = ObjectFactory.GetInstance<IBusiness>();

        public SearchResults Search(string searchKey)
        {
            return _business.Search(searchKey);
        }

        public SearchResults Search(Guid customerId)
        {
            return _business.Search(customerId);
        }

        public Member Save(Member customer)
        {
            var cust = _business.Save(new DataAccess.Customer { Id = customer.Id, Name = customer.Name });
            return new Member { Id = cust.Id, Name = cust.Name };
        }

        public bool Delete(Guid customerId)
        {
            return _business.Delete(customerId);
        }
    }
}
