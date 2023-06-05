using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProfileController : Controller
    {
        UserManager userManager = new UserManager(new EfUserDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = userManager.TList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = userManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(User user)
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(user);

            if (results.IsValid)
            {
                var values = userManager.TGetById(user.Id);

                values.UserName = user.UserName;
                values.Password = user.Password;

                userManager.TUpdate(values);

                TempData["SuccessMessage"] = "Profil başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(user);
        }

    }
}
