using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Post
{
    public class PostComments : ViewComponent
    {
        PostCommentManager postCommentManager = new PostCommentManager(new EfPostCommentDal());

        public IViewComponentResult Invoke()
        {
            int postId = ViewBag.PostId;
            var values = postCommentManager.GetCommentsByPost(postId);
            ViewBag.CommentCount = postCommentManager.GetCommentsByPost(postId).Count;
            return View(values);
        }
    }
}
