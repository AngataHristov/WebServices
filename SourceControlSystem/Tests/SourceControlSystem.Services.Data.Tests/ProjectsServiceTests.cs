namespace SourceControlSystem.Services.Data.Tests
{
    using System;
    using System.Linq;
    using Interfaces;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SourceControlSystem.Data.Models;
    using TestObjects;

    [TestClass]
    public class ProjectsServiceTests
    {
        private InMemoryRepository<User> usersRepository;
        private InMemoryRepository<SoftwareProject> projectsRepository;
        private IProjectsService projectsService;

        [TestInitialize]
        public void Init()
        {
            this.usersRepository = TestObjectFactory.GetUsersRepository();
            this.projectsRepository = TestObjectFactory.GetProjectsRepository();
            this.projectsService = new ProjectsService(this.projectsRepository, this.usersRepository);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddShouldThrowExceptionWithNotFoundUser()
        {
            var project = new SoftwareProject()
            {
                Name = "Test",
                Description = "Test",
                IsPrivate = false,
                CreatedOn = DateTime.Now
            };

            var result = this.projectsService.Add(project, "Invalid User");
        }

        [TestMethod]
        public void AddShouldInvokeSaveChanges()
        {
            var project = new SoftwareProject()
            {
                Name = "Test",
                Description = "Test Description",
                IsPrivate = false,
                CreatedOn = DateTime.Now
            };

            this.projectsService.Add(project, "1");

            Assert.AreEqual(1, this.projectsRepository.NumberOfSaves);
        }

        [TestMethod]
        public void AddShouldPopulateUserAndProjectToDatabase()
        {
            var dbProject = new SoftwareProject()
            {
                Name = "Test",
                Description = "Test Description",
                IsPrivate = false,
                CreatedOn = DateTime.Now
            };

            var result = this.projectsService.Add(dbProject, "1");

            var project = this.projectsRepository
                .All()
                .FirstOrDefault(pr => pr.Name == "Test");

            Assert.IsNotNull(project);
            Assert.AreEqual("Test", project.Name);
            Assert.AreEqual("Test Description", project.Description);
            Assert.AreEqual(1, project.Users.Count);
            Assert.AreEqual(1, project.Users.Count);
            Assert.AreEqual("Test User 1", project.Users.First().UserName);
        }
    }
}
