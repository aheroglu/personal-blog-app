using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
	public class AdminHeader : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
