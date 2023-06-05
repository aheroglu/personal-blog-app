using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Training
{
    public class PopularTrainingsByCategory : ViewComponent
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());
        TrainingCategoryManager trainingCategoryManager = new TrainingCategoryManager(new EfTrainingCategoryDal());

        public IViewComponentResult Invoke()
        {
            int trainingId = ViewBag.TrainingId;
            int trainingCategoryId = trainingManager.TGetById(trainingId).TrainingCategoryId;
            ViewBag.CategoryName = trainingCategoryManager.TGetById(trainingCategoryId).Name;
            var values = trainingManager.PopularPostsByCategory(trainingCategoryId);
            return View(values);
        }
    }
}
