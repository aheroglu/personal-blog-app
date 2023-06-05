using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
	public class AdminHead : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
