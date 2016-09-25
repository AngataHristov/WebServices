namespace SourceControlSystem.Services.Data
{
    using System.Linq;
    using SourceControlSystem.Data.Models;

    using Interfaces;
    using SourceControlSystem.Data.Common.Repositories;
    using Common.Constants;
    using System;

    public class ProjectsService : IProjectsService
    {
        private readonly IRepository<SoftwareProject> projects;
        private readonly IRepository<User> users;

        public ProjectsService(IRepository<SoftwareProject> projectsRepo, IRepository<User> usersRepo)
        {
            this.projects = projectsRepo;
            this.users = usersRepo;
        }

        //public int Add(string name, string description, string creatorId, bool isPrivate = false)
        //{
        //    var currentUser = this.users
        //       .All()
        //       .FirstOrDefault(u => u.Id == creatorId);

        //    var newProject = new SoftwareProject()
        //    {
        //        Name = name,
        //        Description = description,
        //        IsPrivate = isPrivate,
        //        CreatedOn = DateTime.UtcNow
        //    };

        //    newProject.Users.Add(currentUser);

        //    this.projects.Add(newProject);
        //    this.projects.SaveChanges();

        //    return newProject.Id;
        //}

        public int Add(SoftwareProject project, string creatorId)
        {
            var currentUser = this.users
               .All()
               .FirstOrDefault(u => u.Id == creatorId);

            if (currentUser == null)
            {
                throw new ArgumentException("Current user cannot be found");
            }

            project.Users.Add(currentUser);
            project.CreatedOn = DateTime.UtcNow;

            this.projects.Add(project);
            this.projects.SaveChanges();

            return project.Id;
        }

        public IQueryable<SoftwareProject> All(int page = GlobalConstants.DefaultPage, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.projects
                .All()
                .OrderByDescending(p => p.CreatedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return result;
        }

        public IQueryable<SoftwareProject> GetProjectByName(string projectName, string userId)
        {
            var projects = this.projects
                .All()
                .Where(p =>
                p.Name == projectName &&
                (!p.IsPrivate || (p.Users.Any(u => u.Id == userId))))
                .AsQueryable();

            return projects;
        }
    }
}
