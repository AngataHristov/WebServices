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
    [RoutePrefix("api/posts")]
    public class CommentsController : BaseController
    {
        // api/posts/{postId}/comments
        [HttpGet]
        [Route("{postId}/comments")]
        public IHttpActionResult GetPostComments(int postId)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var comments = post.Comments
                .Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    Author = new UserViewModel()
                    {
                        Username = c.Author.UserName
                    }
                });

            return this.Ok(comments);
        }

        //POST api/posts/{postId}/comments/
        [HttpPost]
        [Route("{postId}/comments")]
        public IHttpActionResult AddCommentToPost(int postId, [FromBody]AddCommentBindingModel model)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            if (model == null)
            {
                return this.BadRequest("Model cannot be null!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var loggeUserId = this.User.Identity.GetUserId();

            var comment = new Comment()
            {
                Content = model.Content,
                PostedOn = DateTime.UtcNow,
                AuthorId = loggeUserId
            };

            post.Comments.Add(comment);
            this.Context.SaveChanges();

            return this.Ok();
        }

        //PUT api/posts/{postId}/comments{commentId}
        [HttpPut]
        [Route("{postId}/comments/{commentId}")]
        public IHttpActionResult EditComment(int postId, int commentId, [FromBody]EditCommentBindingModel model)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            if (model == null)
            {
                return this.BadRequest("Model is empty!");
            }

            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var comment = post.Comments
                .FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return this.NotFound();
            }

            var loggedUser = this.User.Identity.GetUserId();

            if (loggedUser != comment.AuthorId)
            {
                return this.Unauthorized();
            }

            comment.Content = model.Content;

            this.Context.SaveChanges();

            var data = this.Context.Comments
                .Where(c => c.Id == comment.Id)
                .Select(c => new CommentViewModel()
                {
                    Id = c.Id,
                    Content = c.Content,
                    Author = new UserViewModel()
                    {
                        Username = c.Author.UserName
                    }
                })
                .FirstOrDefault();

            return this.Ok(data);
        }

        //DELETE api/posts/{postId}/comments{commentId}
        [HttpDelete]
        [Route("{postId}/comments/{commentId}")]
        public IHttpActionResult Delete(int postId, int commentId)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            //if (!this.User.IsInRole("Admin"))
            //{
            //   return this.Unauthorized();
            //}


            var comment = post.Comments
                .FirstOrDefault(c => c.Id == commentId);

            if (comment == null)
            {
                return this.NotFound();
            }

            this.Context.Comments.Remove(comment);
            this.Context.SaveChanges();

            return this.Ok();
        }
    }
}