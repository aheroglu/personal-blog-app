using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.ViewComponents.Home
{
    public class Trainings : ViewComponent
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());

        public IViewComponentResult Invoke()
        {
            var values = trainingManager.ListWithCategory().Take(12);
            return View(values);
        }
    }
}
