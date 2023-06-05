using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Post
{
    public class PopularPostsByCategory : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());
        PostCategoryManager postCategoryManager = new PostCategoryManager(new EfPostCategoryDal());

        public IViewComponentResult Invoke()
        {
            int postId = ViewBag.PostId;
            int postCategoryId = postManager.TGetById(postId).PostCategoryId;
            ViewBag.CategoryName = postCategoryManager.TGetById(postCategoryId).Name;
            var values = postManager.PopularPostsByCategory(postCategoryId);
            return View(values);
        }
    }
}
