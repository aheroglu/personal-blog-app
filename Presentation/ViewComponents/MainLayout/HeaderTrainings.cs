using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.ViewComponents.MainLayout
{
    public class HeaderTrainings : ViewComponent
    {
        TrainingCategoryManager trainingCategoryManager = new TrainingCategoryManager(new EfTrainingCategoryDal());

        public IViewComponentResult Invoke()
        {
            var values = trainingCategoryManager.TList().Where(x => x.Status == true);
            return View(values);
        }
    }
}
