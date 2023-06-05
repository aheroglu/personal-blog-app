using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Home
{
    public class Extra : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
