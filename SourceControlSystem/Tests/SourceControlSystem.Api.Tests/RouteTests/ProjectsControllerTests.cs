namespace SourceControlSystem.Api.Tests.RouteTests
{
    using System.Net.Http;
    using System.Web.Http;

    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.SoftwareProjects.RequestModels;
    using MyTested.WebApi;

    [TestClass]
    public class ProjectsControllerTests
    {
        [TestInitialize]
        public void Init()
        {
           var config=new HttpConfiguration();
            WebApiConfig.Register(config);
            MyWebApi.IsUsing(WebApiConfig.Config);
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("/api/Projects")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{""Name"": ""Test"", ""IsPrivate"": true}")
                .To<ProjectsController>(c => c.Post(new SaveSoftwareProjectRequestModel()
                {
                    Name = "Test",
                    IsPrivate = true
                }));
        }
    }
}
