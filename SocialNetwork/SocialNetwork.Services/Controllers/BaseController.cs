namespace SocialNetwork.Services.Controllers
{
    using System.Web.Http;

    using Data;

    public class BaseController : ApiController
    {
        public BaseController()
            : this(new SocialNetworkDbContext())
        {
        }

        public BaseController(SocialNetworkDbContext data)
        {
            this.Context = data;
        }

        protected SocialNetworkDbContext Context { get; set; }
    }
}