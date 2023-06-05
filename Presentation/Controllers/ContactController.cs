using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Presentation.Controllers
{
    [AllowAnonymous]
    public class ContactController : Controller
    {
        MessageManager messageManager = new MessageManager(new EfMessageDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            MessageValidator validator = new MessageValidator();
            ValidationResult results = validator.Validate(message);

            if (results.IsValid)
            {
                message.BeenRead = false;
                message.Date = DateTime.Now;

                messageManager.TInsert(message);

                TempData["SuccessMessage"] = "Mesajınız başarıyla iletildi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View("Index");
        }

    }
}
