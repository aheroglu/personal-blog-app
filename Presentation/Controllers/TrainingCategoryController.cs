using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class TrainingCategoryController : Controller
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());
        TrainingCategoryManager trainingCategoryManager = new TrainingCategoryManager(new EfTrainingCategoryDal());

        public IActionResult Posts(int id, int page = 1)
        {
            var values = trainingManager.TrainingByCategory(id).ToPagedList(page, 20);
            ViewBag.CategoryName = trainingCategoryManager.TGetById(id).Name;
            return View(values);
        }
    }
}
