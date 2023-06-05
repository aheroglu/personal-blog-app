using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Post
{
    public class PostDetails : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());

        public IViewComponentResult Invoke()
        {
            int postId = ViewBag.PostId;
            var values = postManager.GetPostWithCategory(postId);
            return View(values);
        }
    }
}
