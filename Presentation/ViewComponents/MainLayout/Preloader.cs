using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class Preloader:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
