using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.ViewComponents.AdminDashboard
{
	public class LatestTrainings:ViewComponent
	{
		TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());

		public IViewComponentResult Invoke()
		{
			var values = trainingManager.LatestFiveTraining();
			return View(values);
		}
	}
}
