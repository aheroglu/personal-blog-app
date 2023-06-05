using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class PostCategoryController : Controller
    {
        PostManager postManager = new PostManager(new EfPostDal());
        PostCategoryManager postCategoryManager = new PostCategoryManager(new EfPostCategoryDal());

        public IActionResult Posts(int id, int page = 1)
        {
            var values = postManager.PostsByCategory(id).ToPagedList(page, 20);
            ViewBag.CategoryName = postCategoryManager.TGetById(id).Name;
            return View(values);
        }
    }
}
