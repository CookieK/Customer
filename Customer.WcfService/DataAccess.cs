using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using Newtonsoft.Json.Serialization;
using SisoDb;
using SisoDb.Sql2012;

namespace Customer.WcfService
{

    public interface IDataAccess
    {
        IEnumerable<DataAccess.Customer> Search(string searchKey);
        DataAccess.Customer Save(DataAccess.Customer customer);
        IEnumerable<DataAccess.Customer> Search(Guid customerId);
        bool Delete(Guid customerId);
        void SaveMessage(DataAccess.CustomerMessage message);

    }

    public class SisoDictionary
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class DataAccess : IDataAccess
    {

        public class Customer
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class CustomerMessage
        {
            public Customer From { get; set; }
            public string To { get; set; }
            public string Text { get; set; }
        }

        private ISisoDatabase _db;

        public DataAccess(ISisoDatabase db)
        {
            _db = db;
        }

        public IEnumerable<Customer> Search(string searchKey)
        {
            var dbResult = _db.UseOnceTo().Query<SisoDictionary>().Where(s => s.Name.Contains(searchKey));
            return from result in dbResult.ToListOf<SisoDictionary>()
                   select new Customer { Id = result.Id, Name = result.Name };
        }

        public IEnumerable<Customer> Search(Guid customerId)
        {
            var dbResult = _db.UseOnceTo().Query<SisoDictionary>().Where(s => s.Id == customerId);
            return from result in dbResult.ToListOf<SisoDictionary>()
                   select new Customer { Id = result.Id, Name = result.Name };
        }

        public Customer Save(Customer customer)
        {
            try
            {
                var dictionary = new SisoDictionary();

                if (customer.Id != new Guid())
                {
                    dictionary = new SisoDictionary
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                    };
                    _db.UseOnceTo().Update(dictionary);
                }
                else
                {
                    dictionary = new SisoDictionary
                    {
                        Name = customer.Name,
                    };
                    _db.UseOnceTo().Insert(dictionary);
                }

                return new Customer { Id = dictionary.Id, Name = dictionary.Name };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Delete(Guid customerId)
        {
            try
            {
                _db.UseOnceTo().DeleteById<SisoDictionary>(customerId);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveMessage(CustomerMessage message)
        {
            //try
            //{
            //    var dictionary = new SisoDictionary();

            //    if (message.Id != new Guid())
            //    {
            //        dictionary = new SisoDictionary
            //        {
            //            Id = customer.Id,
            //            Name = customer.Name,
            //        };
            //        _db.UseOnceTo().Update(dictionary);
            //    }
            //    else
            //    {
            //        dictionary = new SisoDictionary
            //        {
            //            Name = customer.Name,
            //        };
            //        _db.UseOnceTo().Insert(dictionary);
            //    }

            //    return new Customer { Id = dictionary.Id, Name = dictionary.Name };
            //}
            //catch (Exception)
            //{
            //    return null;
            //}

            var request = new HttpRequestMessage { RequestUri = new Uri("http://localhost:50383/api/message/ReceiveMessage") };

            request.Method = HttpMethod.Put;
            request.Content = new ObjectContent<CustomerMessage>(message, new JsonMediaTypeFormatter { SerializerSettings = { ContractResolver = new CamelCasePropertyNamesContractResolver() } });

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
            {
                using (var client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    var result = client.SendAsync(request).Result;
                }
            }
        }
    }
}