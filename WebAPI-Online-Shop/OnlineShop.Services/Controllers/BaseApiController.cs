namespace OnlineShop.Services.Controllers
{
    using Data;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        public BaseApiController()
            : this(new OnlineShopDbContext())
        {
        }

        public BaseApiController(OnlineShopDbContext data)
        {
            this.Context = data;
        }

        protected OnlineShopDbContext Context { get; set; }
    }
}