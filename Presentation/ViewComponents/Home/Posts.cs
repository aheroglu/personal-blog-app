using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.ViewComponents.Home
{
    public class Posts : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());

        public IViewComponentResult Invoke()
        {
            var values = postManager.ListWithCategory().Take(12);
            return View(values);
        }
    }
}
