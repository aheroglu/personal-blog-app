using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class MainLayoutController : Controller
    {
        Context db = new Context();
        NewsletterManager newsletterManager = new NewsletterManager(new EfNewsletterDal());
        PostManager postManager = new PostManager(new EfPostDal());

        public PartialViewResult FooterNewsletter()
        {
            return PartialView();
        }

        [HttpGet]
        public IActionResult SignUpNewsletter()
        {
            return View();
        }

        public IActionResult SignUpNewsletter(Newsletter newsletter)
        {
            NewsletterValidator validator = new NewsletterValidator();
            ValidationResult results = validator.Validate(newsletter);

            bool isRegistered = db.Newsletters.Any(x => x.Email == newsletter.Email);

            if (isRegistered)
            {
                TempData["ErrorMessage"] = "Bu e-mail hesabı daha önce bültene kayıt olmuş!";
                return RedirectToAction("Index", "Home");
            }

            if (results.IsValid)
            {
                newsletterManager.TInsert(newsletter);

                TempData["SuccessMessage"] = "Bültene başarıyla kaydolundu!";

                return RedirectToAction("Index", "Home");
            }

            return View("Index");
        }

        public IActionResult Search(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var values = db.Posts.Include(x => x.PostCategory).Where(x => x.Title.Contains(searchTerm) && x.Status == true).ToList();
                return View(values);
            }

            return View();
        }

    }
}
