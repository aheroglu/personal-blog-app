using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
	public class AdminSidebar : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
