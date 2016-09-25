namespace PetStore.Services.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Models.Pets;
    using Data;
    using PetStore.Models;

   // [RoutePrefix("api/some")]
    public class PetsController : ApiController
    {
        PetStoreDbContext context = new PetStoreDbContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return this.Ok(this.context.Pets.ToList());
        }

        //[HttpGet]
        //[Route("another")]
        //public HttpResponseMessage Get(string id)
        //{
        //    return this.Request.CreateResponse(HttpStatusCode.Redirect, "Ivan");
        //}

        public IHttpActionResult Post(int id, PetRequestModel pet)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var newPet = new Pet()
            {
                Name = pet.Name,
                Age = pet.Age
            };

            this.context.Pets.Add(newPet);
            this.context.SaveChanges();

            return this.Ok(pet);
        }

        //[HttpPost]
        //[Route("first")]
        //public IHttpActionResult Post(int id, [FromUri]PetRequestModel pet)
        //{
        //    return this.Ok(pet);
        //}

        //[HttpGet]
        //[AcceptVerbs("Pesho")]
        //public IHttpActionResult SomeOtherAction()
        //{
        //    return this.Ok(true);
        //}
    }
}