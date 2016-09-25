namespace SocialNetwork.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.Description;
    using System.Web.OData;

    using Models.ViewModels;
    using Models.BindingModels;

    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : BaseController
    {
        //GET api/users/{username}/wall
        [HttpGet]
        [Route("{username}/wall")]
        [EnableQuery] // odata -( http://localhost:63009/api/users/angel/wall?$filter=LikesCount gt 5&$skip=2&$top=2& )
        [ResponseType(typeof(IQueryable<PostViewModel>))]
        public IHttpActionResult GetUserWall(string username)
        {
            var user = this.Context.Users
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                return this.NotFound();
            }

            var userWall = this.Context.Posts
                .Where(p => p.AuthorId == user.Id)
                .Select(PostViewModel.Create);

            return this.Ok(userWall);
        }

        // GET api/users/search?name=..&ageMin=..&ageMax=..&location...
        [HttpGet]
        [ActionName("search")]
        public IHttpActionResult SearchUser([FromUri]UserSearchBindingModel model)
        {
            if (model == null)
            {
                return this.BadRequest("Model cannot be null(no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var usersSeatchResult = this.Context.Users.AsQueryable();

            if (model.Name != null)
            {
                usersSeatchResult = usersSeatchResult
                   .Where(u => u.UserName.Contains(model.Name));
            }

            if (model.MinAge.HasValue)
            {
                usersSeatchResult = usersSeatchResult
                  .Where(u => u.Age >= model.MinAge.Value);
            }

            if (model.MaxAge.HasValue)
            {
                usersSeatchResult = usersSeatchResult
                  .Where(u => u.Age <= model.MaxAge.Value);
            }

            if (model.Location != null)
            {
                usersSeatchResult = usersSeatchResult
                  .Where(u => u.Location == model.Location);
            }

            var finalResult = usersSeatchResult
                .Select(u => new
                {
                    u.UserName,
                    u.Age
                });

            return this.Ok(finalResult);
        }
    }
}