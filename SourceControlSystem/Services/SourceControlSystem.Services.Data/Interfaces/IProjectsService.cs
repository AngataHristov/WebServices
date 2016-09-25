namespace SourceControlSystem.Services.Data.Interfaces
{
    using System.Linq;

    using Common.Constants;
    using SourceControlSystem.Data.Models;

    public interface IProjectsService
    {
        IQueryable<SoftwareProject> All(int page = 1, int pageSize = GlobalConstants.DefaultPageSize);

       // int Add(string name, string description, string creatorId, bool isPrivate = false);
        int Add(SoftwareProject project, string creatorId);

        IQueryable<SoftwareProject> GetProjectByName(string projectName, string userId);
    }
}
