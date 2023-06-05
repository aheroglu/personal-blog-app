using DataAccess.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        Context db = new Context();

        public IActionResult Index()
        {
            ViewBag.PostCount = db.Posts.Where(x => x.Status == true && x.PostCategory.Status == true).Count();
            ViewBag.TrainingCount = db.Trainings.Where(x => x.Status == true && x.TrainingCategory.Status == true).Count();
            ViewBag.NewsletterCount = db.Newsletters.Count();

            return View();
        }

    }
}
