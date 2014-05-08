using System.Web.Http;

namespace Customer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Add any additional configuration code.

            // Web API routes
            config.MapHttpAttributeRoutes();

             config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.Routes.MapHttpRoute(
                      name: "WithActionApi",
                      routeTemplate: "api/{controller}/{action}/{id}",
                      defaults: new { id = RouteParameter.Optional }
                  );

           
        }
    }
}