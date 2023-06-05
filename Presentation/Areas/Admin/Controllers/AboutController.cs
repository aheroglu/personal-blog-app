using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        AboutManager aboutManager = new AboutManager(new EfAboutDal());

        public IActionResult Index()
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = aboutManager.TList();
            return View(values);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var values = aboutManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(About about, IFormFile image)
        {
            AboutValidator validator = new AboutValidator();
            ValidationResult results = validator.Validate(about);

            if (results.IsValid)
            {
                var values = aboutManager.TGetById(about.Id);

                if (image != null && image.Length > 0)
                {
                    // Delete Current Image
                    if (values.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
                    {
                        string currentImage = values.Image;
                        string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/About/", currentImage);
                        System.IO.File.Delete(currentImagePath);
                    }

                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/About/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var stream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }

                    values.Image = guidFileName;
                }

                else
                {
                    values.Image = values.Image;
                }


                values.Title = about.Title;
                values.Content = about.Content;

                aboutManager.TUpdate(values);

                TempData["SuccessMessage"] = "Hakkımda bilgileri başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            return View(about);
        }

    }
}
