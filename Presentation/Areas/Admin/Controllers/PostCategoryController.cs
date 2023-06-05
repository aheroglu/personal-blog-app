using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.WebSockets;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostCategoryController : Controller
    {
        PostCategoryManager postCategoryManager = new PostCategoryManager(new EfPostCategoryDal());
        PostManager postManager = new PostManager(new EfPostDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = postCategoryManager.TList().Where(x => x.Status == true).ToPagedList(page, 10);
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(PostCategory postCategory)
        {
            PostCategoryValidator validator = new PostCategoryValidator();
            ValidationResult results = validator.Validate(postCategory);

            if (results.IsValid)
            {
                postCategory.Status = true;

                postCategoryManager.TInsert(postCategory);

                TempData["SuccessMessage"] = "Blog kategorisi başarıyla eklendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(postCategory);
        }

        public IActionResult Delete(int id)
        {
            var values = postCategoryManager.TGetById(id);
            values.Status = false;
            postCategoryManager.TUpdate(values);

            TempData["SuccessMessage"] = "Blog kategorisi başarıyla silindi";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = postCategoryManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(PostCategory postCategory)
        {
            PostCategoryValidator validator = new PostCategoryValidator();
            ValidationResult results = validator.Validate(postCategory);

            if (results.IsValid)
            {
                var values = postCategoryManager.TGetById(postCategory.Id);
                values.Name = postCategory.Name;
                postCategoryManager.TUpdate(values);

                TempData["SuccessMessage"] = "Blog kategorisi başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(postCategory);
        }

        public IActionResult Posts(int id, int page = 1)
        {
            var values = postManager.ListWithCategory().Where(x => x.PostCategoryId == id).ToPagedList(page, 10);
            ViewBag.CategoryName = postCategoryManager.TGetById(id).Name;
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedCategories)
        {
            foreach (var categoryId in selectedCategories)
            {
                var category = postCategoryManager.TGetById(categoryId);
                category.Status = false;
                postCategoryManager.TUpdate(category);
            }

            TempData["SuccessMessage"] = "Seçilen kategoriler başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
