using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Customer.Controllers.Api;
using Customer.WcfService;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Customer.Container
{
    public class ClassRegistry : Registry
    {
        public ClassRegistry()
        {
            //ObjectFactory.Initialize(x =>
            //{
            //    x.For<IDataAccess>().Use(new DataAccess());

            //    x.For<IBusiness>().Use<Business>();
            //});
        }
    }
}