using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
    public class Notification : ViewComponent
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());

        public IViewComponentResult Invoke()
        {
            ViewBag.UnreadMessageCount = messageManager.TList().Where(x => x.BeenRead == false).Count();
            return View();
        }
    }
}
