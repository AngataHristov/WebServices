namespace SocialNetwork.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;

    using SocialNetwork.Models;

    [Authorize]
    [RoutePrefix("api/posts")]
    public class PostLikesController : BaseController
    {
        // api/post/{postId}/likes
        [HttpGet]
        [Route("{postId}/likes")]
        public IHttpActionResult GetPostLikes(int postId)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var likes = post.Likes
                .Select(l => new
                {
                    Id = l.Id,
                    Author = l.User.UserName
                });

            return this.Ok(likes);
        }

        // api/post/{postId}/likes
        [HttpPost]
        [Route("{postId}/likes")]
        public IHttpActionResult LikePost(int postId)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUser = this.User.Identity.GetUserId();

            var isAlreadyLiked = post.Likes
                .Any(l => l.UserId == loggedUser);

            if (isAlreadyLiked)
            {
                return this.BadRequest("Already liked by this user");
            }

            if (post.AuthorId == loggedUser)
            {
                return this.BadRequest("Cannot like own posts");
            }

            post.Likes.Add(new PostLike()
            {
                UserId = loggedUser
            });

            this.Context.SaveChanges();

            return this.Ok();
        }

        //DELETE api/post/{postId}/likes
        [Route("{postId}/likes")]
        [HttpDelete]
        public IHttpActionResult DeletePostLike(int postId)
        {
            var post = this.Context.Posts
                .Find(postId);

            if (post == null)
            {
                return this.NotFound();
            }

            var loggedUserId = this.User.Identity.GetUserId();

            var like = post.Likes
                .FirstOrDefault(l => l.UserId == loggedUserId);

            if (like == null)
            {
                return this.BadRequest();
            }

            this.Context.Likes.Remove(like);
            
            this.Context.SaveChanges();
           
            return this.Ok();
        }
    }
}