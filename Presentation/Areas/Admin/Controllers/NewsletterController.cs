using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterController : Controller
    {
        NewsletterManager newsletterManager = new NewsletterManager(new EfNewsletterDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = newsletterManager.TList().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var values = newsletterManager.TGetById(id);
            newsletterManager.TDelete(values);
            TempData["SuccessMessage"] = "E-Mail başarıyla silindi";
            return RedirectToAction("Index");
        }

        public IActionResult DeleteSelected(int[] selectedMails)
        {
            foreach (var mailId in selectedMails)
            {
                var mail = newsletterManager.TGetById(mailId);
                newsletterManager.TDelete(mail);
            }

            TempData["SuccessMessage"] = "Seçilen E-Mailler başarıyla silindi";

            return RedirectToAction("Index");
        }


    }
}
