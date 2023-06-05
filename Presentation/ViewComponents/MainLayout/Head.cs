using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class Head : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
