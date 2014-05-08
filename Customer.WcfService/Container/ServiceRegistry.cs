using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SisoDb;
using SisoDb.Sql2012;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace Customer.WcfService.Container
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry ()
        {
            ISisoDatabase db = "CustomerSisoDb".CreateSql2012Db();
            db.CreateIfNotExists();

            ObjectFactory.Initialize(x =>
            {
                x.For<IDataAccess>().Use(new DataAccess(db));

                x.For<IBusiness>().Use<Business>();
            });
        }
    }
}