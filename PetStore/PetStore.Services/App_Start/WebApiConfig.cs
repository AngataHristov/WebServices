namespace PetStore.Services
{
    using System.Web.Http;
    using System.Web.Http.Routing;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                //constraints: new { id = @"[\d]{1,4}" },
                //handler: new StopRoutingHandler()
            );
        }
    }
}
