namespace SourceControlSystem.Api.Tests.ControllerTests
{
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models.SoftwareProjects.RequestModels;
    using Models.SoftwareProjects.ResponseModels;
    using MyTested.WebApi;
    using Services.Data.Interfaces;

    [TestClass]
    public class ProjectsControllerTests
    {
        [TestMethod]
        public void PostShouldReturnBadRequestWithNullModel()
        {
            var controller = new ProjectsController(TestObjectFactory.GetProjectService());
            controller.Configuration = new HttpConfiguration();

            SaveSoftwareProjectRequestModel model = null;

            controller.Validate(model);

            var result = controller.Post(model);

            Assert.AreEqual(typeof(BadRequestErrorMessageResult), result.GetType());
        }

        [TestMethod]
        public void PostShouldValidateModelState()
        {
            MyWebApi
                .Controller<ProjectsController>()
                .WithResolvedDependencyFor(TestObjectFactory.GetProjectService())
                .Calling(c => c.Post(TestObjectFactory.GetInvalidModel()))
                .ShouldHave()
                .InvalidModelState();

            //var controller = new ProjectsController(TestObjectFactory.GetProjectService())
            //{
            //    Configuration = new HttpConfiguration()
            //};

            //var model = TestObjectFactory.GetInvalidModel();

            //controller.Validate(model);

            //Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void PostShouldReturnBadRequestWithInvalidModel()
        {
            MyWebApi
                .Controller<ProjectsController>()
                .WithResolvedDependencyFor<IProjectsService>(TestObjectFactory.GetProjectService())
                .Calling(c => c.Get())
                .ShouldHave()
                .ActionAttributes(attr => attr.RestrictingForAuthorizedRequests())
                .AndAlso()
                .ShouldReturn()
                .Ok()
                .WithResponseModelOfType<List<SoftwareProjectDetailsResponseModel>>();

            //var controller = new ProjectsController(TestObjectFactory.GetProjectService());
            //controller.Configuration = new HttpConfiguration();

            //var model = TestObjectFactory.GetInvalidModel();

            //controller.Validate(model);

            //var result = controller.Post(model);

            //Assert.AreEqual(typeof(InvalidModelStateResult), result.GetType());
        }
    }
}
