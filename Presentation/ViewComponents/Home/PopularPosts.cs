using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Home
{
    public class PopularPosts : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());
        
        public IViewComponentResult Invoke()
        {
            var values = postManager.PopularPostsList();
            return View(values);
        }
    }
}
