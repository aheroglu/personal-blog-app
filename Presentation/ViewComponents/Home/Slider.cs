using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.ViewComponents.Home
{
    public class Slider : ViewComponent
    {
        PostManager postManager = new PostManager(new EfPostDal());

        public IViewComponentResult Invoke()
        {
            var values = postManager.ListWithCategory().OrderByDescending(x => x.Id).Take(3);
            return View(values);
        }
    }
}
