using System;
using System.Collections.Generic;
using System.Linq;
using Customer.WcfService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Customer.Test
{
    [TestClass]
    public class DataAccessTest
    {
        [TestMethod]
        public void RhinoMockedBusinessReturnsData()
        {
            var storage = new Mock<IDataAccess>();

            var searchResults = new List<DataAccess.Customer> { new DataAccess.Customer { Id  = new Guid(), Name = "Test"} };
            storage.Setup(x => x.Search("Test")).Returns(searchResults);

            var controllerUnderTest = new Business(storage.Object);

            Assert.AreEqual(1, controllerUnderTest.Search("Test").Customers.Count());
        }
    }
}
