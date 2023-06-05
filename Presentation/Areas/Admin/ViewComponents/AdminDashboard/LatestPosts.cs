using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminDashboard
{
    public class LatestPosts : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());

        public IViewComponentResult Invoke()
        {
            var values = postManager.LatestFivePost();
            return View(values);
        }
    }
}
