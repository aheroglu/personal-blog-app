using Business.Concrete;
using Business.ValidationRules;
using DataAccess.Concrete;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json.Linq;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainingController : Controller
    {
        MailService _mailService;

        public TrainingController(MailService mailService)
        {
            _mailService = mailService;
        }

        Context db = new Context();
        TrainingManager trainingManager = new TrainingManager(new EfTrainingDal());
        TrainingCategoryManager trainingCategoryManager = new TrainingCategoryManager(new EfTrainingCategoryDal());
        TrainingCommentManager trainingCommentManager = new TrainingCommentManager(new EfTrainingCommentDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = trainingManager.ListWithCategory().ToPagedList(page, 10);
            return View(values);
        }

        [HttpPost]
        public IActionResult Index(string searchTerm, int page = 1)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var values = trainingManager.ListWithCategory().Where(x => x.Title.ToLower().Contains(searchTerm)).ToPagedList(page, 10);
              
                if (values.Count == 0)
                {
                    ViewBag.SearchError = "Aranan değer ile ilgili kayıt bulunamadı!";
                }
               
                return View(values);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> categories = (from x in trainingCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Training training, IFormFile image)
        {
            TrainingValidator validator = new TrainingValidator();
            ValidationResult results = validator.Validate(training);

            if (results.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Training/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    training.Image = guidFileName;
                }

                else
                {
                    training.Image = "/Templates/admin-template/assets/images/defaul-post-image.png";
                }

                training.Status = true;
                training.Date = DateTime.Now;
                training.ClickCount = 0;

                trainingManager.TInsert(training);

                // Send Mail To Registered Users
                _mailService.SendEmailForNewTraining(training.Title, training.Id);

                TempData["SuccessMessage"] = "Eğitim başarıyla eklendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            List<SelectListItem> categories = (from x in trainingCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View(training);
        }

        public IActionResult Delete(int id)
        {
            var values = trainingManager.TGetById(id);
            values.Status = false;
            trainingManager.TUpdate(values);

            if (values.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
            {
                // Delete Current Image In Direction
                string currentImage = values.Image;
                string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Training/", currentImage);
                System.IO.File.Delete(currentImagePath);
            }

            TempData["SuccessMessage"] = "Eğitim başarıyla silindi";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> categories = (from x in trainingCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            var values = trainingManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(Training training, IFormFile image)
        {
            TrainingValidator validator = new TrainingValidator();
            ValidationResult results = validator.Validate(training);

            if (results.IsValid)
            {
                var values = trainingManager.TGetById(training.Id);

                if (image != null && image.Length > 0)
                {
                    if (values.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
                    {
                        // Delete Current Image In Direction
                        string currentImage = values.Image;
                        string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Training/", currentImage);
                        System.IO.File.Delete(currentImagePath);
                    }

                    // Save New Image
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Training/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    values.Image = guidFileName;
                }

                else
                {
                    values.Image = values.Image;
                }

                values.Title = training.Title;
                values.TrainingCategoryId = training.TrainingCategoryId;
                values.Content = training.Content;

                trainingManager.TUpdate(values);

                TempData["SuccessMessage"] = "Eğitim başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            List<SelectListItem> categories = (from x in trainingCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View(training);
        }

        public IActionResult Details(int id)
        {
            var values = trainingManager.GetTrainingWithCategory(id);
            return View(values);
        }

        public IActionResult Comments(int id, int page = 1)
        {
            var values = trainingCommentManager.GetCommentsByPost(id).ToPagedList(page, 10);
            ViewBag.TrainingTitle = trainingManager.TGetById(id).Title;
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedTrainings)
        {
            foreach (var trainingId in selectedTrainings)
            {
                var training = trainingManager.TGetById(trainingId);
                training.Status = false;
                trainingManager.TUpdate(training);

                if (training.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
                {
                    // Delete Current Image In Direction
                    string currentImage = training.Image;
                    string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Training/", currentImage);
                    System.IO.File.Delete(currentImagePath);
                }
            }

            TempData["SuccessMessage"] = "Seçilen eğitimler başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
