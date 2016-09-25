namespace OnlineShop.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;

    using Models;

    using OnlineShop.Models;
    using System;

    // [Authorize]
    public class AdsController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetAds()
        {
            var ads = this.Context.Ads
                .OrderBy(a => a.Type.Name)
                .ThenByDescending(a => a.PostedOn)
                .Select(a => new
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    OwnerId = a.Owner.Id,
                    OwnerUserName = a.Owner.UserName,
                    Type = a.Type.Name,
                    PostedOn = a.PostedOn,
                    Status = a.Status,
                    Categories = a.Categories
                        .Select(c => new
                        {
                            Id = c.Id,
                            Name = c.Name
                        }).ToList()
                })
                .ToList();

            return this.Ok(ads);
        }

        [HttpPost]
        public IHttpActionResult CreateAd(CreateAdBindingModel model)
        {
            var userId = this.User.Identity.GetUserId();
            if (userId == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var categories = this.Context.Categories
                .Where(c => model.Categories.Contains(c.Id))
                .ToList();

            var ad = new Ad()
            {
                Name = model.Name,
                Description = model.Description,
                TypeId = model.TypeId,
                Price = model.Price,
                Categories = categories,
                OwnerId = userId,
                PostedOn = DateTime.UtcNow,
                Status = AdStatus.Open

            };

            this.Context.Ads.Add(ad);
            try
            {

                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }

            var result = this.Context.Ads
                .Where(a => a.Id == ad.Id)
                .Select(AdViewModel.Create)
                .FirstOrDefault();

            return this.Ok(result);
        }

        [HttpPut]
        [Route("api/ads/{id}/close")]
        public IHttpActionResult CloseAd(int id)
        {
            Ad ad = this.Context.Ads
                .FirstOrDefault(a => a.Id == id);

            if (ad == null)
            {
                return this.NotFound();
            }

            string userId = this.User.Identity.GetUserId();

            if (ad.OwnerId != userId)
            {
                return this.BadRequest();
            }

            ad.Status = AdStatus.Closed;
            ad.ClosedOn = DateTime.UtcNow;
            this.Context.SaveChanges();

            return this.Ok();
        }
    }
}