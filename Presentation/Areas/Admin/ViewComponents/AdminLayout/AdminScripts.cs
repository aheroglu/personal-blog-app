using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminLayout
{
	public class AdminScripts : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
