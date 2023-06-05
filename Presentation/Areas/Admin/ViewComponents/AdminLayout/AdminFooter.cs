using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
	public class AdminFooter : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
