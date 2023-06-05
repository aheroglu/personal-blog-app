using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Training
{
    public class TrainingDetails : ViewComponent
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());

        public IViewComponentResult Invoke()
        {
            int trainingId = ViewBag.TrainingId;
            var values = trainingManager.GetTrainingWithCategory(trainingId);
            return View(values);
        }
    }
}
