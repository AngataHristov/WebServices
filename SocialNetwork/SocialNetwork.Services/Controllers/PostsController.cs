namespace SocialNetwork.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;

    using Models.ViewModels;
    using Models.BindingModels;

    [Authorize]
    public class PostsController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult ExtractPosts()
        {
            var data = this.Context.Posts
                .OrderBy(p => p.PostedOn)
                .Select(PostViewModel.Create);

            return this.Ok(data);
        }

        [HttpPost]
        public IHttpActionResult AddPost([FromBody]AddPostBindingModel model)
        {
            var loggedUserId = this.User.Identity.GetUserId();

            //if (loggedUserId == null)
            //{
            //    return this.Unauthorized();
            //}

            if (model == null)
            {
                return this.BadRequest("Model cannot be null(no data in request)");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var wallOwner = this.Context.Users
                .FirstOrDefault(u => u.UserName == model.WallOwnerUsername);

            if (wallOwner == null)
            {
                return this.BadRequest($"User {model.WallOwnerUsername} does not exist!");
            }

            var post = new Post()
            {
                AuthorId = loggedUserId,
                WallOwner = wallOwner,
                Content = model.Content,
                PostedOn = DateTime.UtcNow
            };

            this.Context.Posts.Add(post);
            this.Context.SaveChanges();

            var data = this.Context.Posts
                .Where(p => p.Id == post.Id)
                .Select(PostViewModel.Create)
                .FirstOrDefault();


            return this.Ok(data);
        }

        // PUT api/posts/{id}
        [HttpPut]
        public IHttpActionResult EditPost(int id, [FromBody]EditPostBindingModel model)
        {
            var post = this.Context.Posts
                .Find(id);

            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            if (loggedUserId != post.AuthorId)
            {
                return this.Unauthorized();
            }

            if (model == null)
            {
                return this.BadRequest("Model is empty!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            post.Content = model.Content;

            this.Context.SaveChanges();

            var data = this.Context.Posts
                .Where(p => p.Id == post.Id)
                .Select(PostViewModel.Create)
                .FirstOrDefault();

            return this.Ok(data);
        }

        //DELETE api/posts/{id}
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var post = this.Context.Posts
                .Find(id);

            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            if (loggedUserId != post.AuthorId &&
                loggedUserId != post.WallOwnerId &&
                !this.User.IsInRole("Admin"))
            {
                this.Unauthorized();
            }

            this.Context.Posts.Remove(post);
            this.Context.SaveChanges();

            return this.Ok();
        }
    }
}