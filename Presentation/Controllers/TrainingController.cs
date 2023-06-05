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
    public class TrainingController : Controller
    {
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());
        TrainingCommentManager trainingCommentManager = new TrainingCommentManager(new EfTrainingCommentDal());

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            ViewBag.TrainingId = id;
            trainingId = id;

            var training = trainingManager.TGetById(id);
            training.ClickCount = training.ClickCount + 1;
            trainingManager.TUpdate(training);

            return View();
        }

        public static int trainingId;

        [HttpPost]
        public IActionResult Details(TrainingComment trainingComment)
        {
            TrainingCommentValidator validator = new TrainingCommentValidator();
            ValidationResult results = validator.Validate(trainingComment);

            if (results.IsValid)
            {
                trainingComment.Id = 0;
                trainingComment.TrainingId = trainingId;
                trainingComment.Date = DateTime.Now;
                trainingComment.Status = true;

                trainingCommentManager.TInsert(trainingComment);

                TempData["SuccessMessage"] = "Yorumunuz başarıyla kaydedildi";

                return RedirectToAction("Details", new { id = trainingComment.TrainingId });
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            ViewBag.TrainingId = trainingId;

            return View(trainingComment);
        }

    }
}
