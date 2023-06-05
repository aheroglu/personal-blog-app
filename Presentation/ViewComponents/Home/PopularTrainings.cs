using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Home
{
    public class PopularTrainings : ViewComponent
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());

        public IViewComponentResult Invoke()
        {
            var values = trainingManager.PopularTrainingsList();
            return View(values);
        }
    }
}
