using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class Header:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
