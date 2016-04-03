using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Customer.WcfService
{
    public interface IBusiness
    {
        SearchResults Search(string searchKey);
        SearchResults Search(Guid searchKey);
        DataAccess.Customer Save(DataAccess.Customer customer);
        void SaveMessage(DataAccess.CustomerMessage message);
        bool Delete(Guid customerId);
    }

    public class Business : IBusiness
    {
        private IDataAccess _dataAccess;

        public Business(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public SearchResults Search(string searchKey)
        {
            return new SearchResults
            {
                Customers = (from list in _dataAccess.Search(searchKey) select list).ToList()
            };
        }

        public SearchResults Search(Guid customerId)
        {
            return new SearchResults
            {
                Customers = (from list in _dataAccess.Search(customerId) select list).ToList()
            };
        }

        public DataAccess.Customer Save(DataAccess.Customer customer)
        {
            return _dataAccess.Save(customer);
        }

        public void SaveMessage(DataAccess.CustomerMessage message)
        {
            _dataAccess.SaveMessage(message);
        }

        public bool Delete(Guid customerId)
        {
            return _dataAccess.Delete(customerId);
        }
    }
}