using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        AccountManager accountManager = new AccountManager(new EfAccountDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = accountManager.TList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Account account)
        {
            AccountValidator validator = new AccountValidator();
            ValidationResult results = validator.Validate(account);

            if (results.IsValid)
            {
                accountManager.TInsert(account);
                TempData["SuccessMessage"] = "Hesap başarıyla eklendi";
                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(account);
        }

        public IActionResult Delete(int id)
        {
            var values = accountManager.TGetById(id);
            accountManager.TDelete(values);
            TempData["SuccessMessage"] = "Hesap başarıyla silindi";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = accountManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(Account account)
        {
            AccountValidator validator = new AccountValidator();
            ValidationResult results = validator.Validate(account);

            if (results.IsValid)
            {
                var values = accountManager.TGetById(account.Id);

                values.Link = account.Link;
                values.Icon = account.Icon;

                accountManager.TUpdate(values);

                TempData["SuccessMessage"] = "Hesap başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(account);
        }

        public IActionResult DeleteSelected(int[] selectedAccounts)
        {
            foreach (var accountId in selectedAccounts)
            {
                var account = accountManager.TGetById(accountId);
                accountManager.TDelete(account);
            }

            TempData["SuccessMessage"] = "Seçilen hesaplar başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
