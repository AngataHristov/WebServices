namespace SourceControlSystem.Api.Tests
{
    using Data.Models;
    using Models.SoftwareProjects.RequestModels;
    using Moq;
    using Services.Data.Interfaces;

    public static class TestObjectFactory
    {
        public static IProjectsService GetProjectService()
        {
            var projectService = new Mock<IProjectsService>();
            projectService
                .Setup(
                    pr => pr.Add(
                        It.IsAny<SoftwareProject>(),
                        It.IsAny<string>()))
                .Returns(1);

            return projectService.Object;
        }

        public static SaveSoftwareProjectRequestModel GetInvalidModel()
        {
            return new SaveSoftwareProjectRequestModel()
            {
                Description = "TestDescription"
            };
        }
    }
}
