namespace PetStore.Services.Controllers
{
    using System.Web.Http;

    public class CategoriesController : ApiController
    {
        public string Get()
        {
            return "pesho";
        }

        public bool Get(int id, string name)
        {
            return true;
        }
    }
}