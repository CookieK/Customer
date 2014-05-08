using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http.WebHost;
using Customer.WcfService;
using StructureMap;
using StructureMap.Graph;

namespace Customer
{
    public class Global : System.Web.HttpApplication
    {
        private void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                     name: "Default",
                     url: "{controller}/{action}/{id}",
                     defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, bsid = "" }
                 );
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            ObjectFactory.Container.AssertConfigurationIsValid();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            RegisterRoutes(RouteTable.Routes);

            ObjectFactory.Configure(x =>
             {
                 x.Scan(scan =>
                 {
                     scan.AssembliesFromApplicationBaseDirectory();
                     scan.LookForRegistries();
                 });
             });

            GlobalConfiguration.Configuration.EnsureInitialized();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}