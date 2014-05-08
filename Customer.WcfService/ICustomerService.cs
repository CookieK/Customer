using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Customer.WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICustomerService
    {
        [OperationContract]
        Member Save(Member customer);

        [OperationContract]
        SearchResults Search(string searchKey);

        [OperationContract]
        bool Delete(Guid customerId);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Values
    {
        [DataMember]
        public string Key { get; set; }
        [DataMember]
        public string Value { get; set; }
    }

    [DataContract]
    public class Member
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class SearchResults
    {
        [DataMember]
        public IList<DataAccess.Customer> Customers { get; set; }
    }
}
