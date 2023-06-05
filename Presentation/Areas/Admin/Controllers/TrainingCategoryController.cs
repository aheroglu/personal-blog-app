using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainingCategoryController : Controller
    {
        TrainingCategoryManager trainingCategoryManager = new TrainingCategoryManager(new EfTrainingCategoryDal());
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = trainingCategoryManager.TList().Where(x => x.Status == true).ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(TrainingCategory trainingCategory)
        {
            TrainingCategoryValidator validator = new TrainingCategoryValidator();
            ValidationResult results = validator.Validate(trainingCategory);

            if (results.IsValid)
            {
                trainingCategory.Status = true;

                trainingCategoryManager.TInsert(trainingCategory);

                TempData["SuccessMessage"] = "Eğitim kategorisi başarıyla eklendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(trainingCategory);
        }

        public IActionResult Delete(int id)
        {
            var values = trainingCategoryManager.TGetById(id);
            values.Status = false;
            trainingCategoryManager.TUpdate(values);
            TempData["SuccessMessage"] = "Eğitim kategorisi başarıyla silindi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = trainingCategoryManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(TrainingCategory trainingCategory)
        {
            TrainingCategoryValidator validator = new TrainingCategoryValidator();
            ValidationResult results = validator.Validate(trainingCategory);

            if (results.IsValid)
            {
                var values = trainingCategoryManager.TGetById(trainingCategory.Id);
                values.Name = trainingCategory.Name;
                trainingCategoryManager.TUpdate(values);

                TempData["SuccessMessage"] = "Eğitim kategorisi başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(trainingCategory);
        }

        public IActionResult Posts(int id, int page = 1)
        {
            var values = trainingManager.ListWithCategory().Where(x => x.TrainingCategoryId == id).ToPagedList(page, 10);
            ViewBag.CategoryName = trainingCategoryManager.TGetById(id).Name;
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedCategories)
        {
            foreach (var categoryId in selectedCategories)
            {
                var category = trainingCategoryManager.TGetById(categoryId);
                category.Status = false;
                trainingCategoryManager.TUpdate(category);
            }

            TempData["SuccessMessage"] = "Seçilen kategoriler başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
