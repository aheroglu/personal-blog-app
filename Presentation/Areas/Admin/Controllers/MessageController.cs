using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = messageManager.TList().Where(x => x.BeenRead == false).OrderByDescending(x => x.Id).ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Details(int id)
        {
            var values = messageManager.MessageById(id);
            return View(values);
        }

        public IActionResult SaveAsRead(int id)
        {
            var values = messageManager.TGetById(id);
            values.BeenRead = true;
            messageManager.TUpdate(values);
            TempData["SuccessMessage"] = "Mesaj okundu olarak kaydedildi";
            return RedirectToAction("Index");
        }

    }
}
