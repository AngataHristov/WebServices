namespace SourceControlSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.AspNet.Identity;

    using Common.Constants;
    using Data.Models;
    using Infrastructure.Mapping;
    using Models.SoftwareProjects.RequestModels;
    using Models.SoftwareProjects.ResponseModels;
    using Services.Data.Interfaces;

    [Authorize]
    [RoutePrefix("api/projects")]
    public class ProjectsController : ApiController
    {
        private readonly IProjectsService projectService;

        public ProjectsController(IProjectsService projectService)
        {
            this.projectService = projectService;
        }


        [AllowAnonymous]
        [EnableCors("*", "*", "*")]
        public IHttpActionResult Get()
        {
            var result = this.projectService
                .All(page: 1)
                .To<SoftwareProjectDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }

        public IHttpActionResult Get(string id)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            if (string.IsNullOrEmpty(id))
            {
                return this.BadRequest("Project name cannot be null ot empty");
            }

            var result = this.projectService
                .GetProjectByName(id, loggedUserId)
                .To<SoftwareProjectDetailsResponseModel>()
                .FirstOrDefault();

            if (result == null)
            {
                return this.NotFound();
            }

            return this.Ok(result);
        }

        public IHttpActionResult Post(SaveSoftwareProjectRequestModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null(no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var loggedUserId = this.User.Identity.GetUserId();

            var dbProject = AutoMapperConfig.Configuration
              .CreateMapper()
              .Map<SoftwareProject>(model);

            var createdProjectId = this.projectService.Add(dbProject, loggedUserId);

            //var createdProjectId = this.projectService.Add(
            //    model.Name,
            //    model.Description,
            //    loggedUserId,
            //    model.IsPrivate);

            return this.Ok(createdProjectId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get(int page, int pageSize = GlobalConstants.DefaultPageSize)
        {
            var result = this.projectService
                .All(page, pageSize)
                .To<SoftwareProjectDetailsResponseModel>()
                .ToList();

            return this.Ok(result);
        }
    }
}