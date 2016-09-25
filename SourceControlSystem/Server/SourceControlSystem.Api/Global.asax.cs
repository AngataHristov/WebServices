namespace SourceControlSystem.Api
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMappingConfig.Config();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
