using Business.Concrete;
using Business.ValidationRules;
using DataAccess.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PostController : Controller
    {
        MailService _mailService;

        public PostController(MailService mailService)
        {
            _mailService = mailService;
        }

        PostManager postManager = new PostManager(new EfPostDal());
        PostCategoryManager postCategoryManager = new PostCategoryManager(new EfPostCategoryDal());
        PostCommentManager postCommentManager = new PostCommentManager(new EfPostCommentDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = postManager.ListWithCategory().ToPagedList(page, 10);
            return View(values);
        }

        [HttpPost]
        public IActionResult Index(string searchTerm, int page = 1)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var values = postManager.ListWithCategory().Where(x => x.Title.ToLower().Contains(searchTerm)).ToPagedList(page, 10);

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
            List<SelectListItem> categories = (from x in postCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View();
        }

        [HttpPost]
        public IActionResult Add(Post post, IFormFile image)
        {
            PostValidator validator = new PostValidator();
            ValidationResult results = validator.Validate(post);

            if (results.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Post/");
                    var createImage = Path.Combine(filePath, guidFileName);

                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    using (var fileStream = new FileStream(createImage, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }

                    post.Image = guidFileName;
                }

                else
                {
                    post.Image = "/Templates/admin-template/assets/images/defaul-post-image.png";
                }

                post.ClickCount = 0;
                post.Date = DateTime.Now;
                post.Status = true;

                postManager.TInsert(post);

                // Send Mail To Registered Users
                _mailService.SendEmailForNewPost(post.Title, post.Id);

                TempData["SuccessMessage"] = "Blog başarıyla eklendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            List<SelectListItem> categories = (from x in postCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View();
        }

        public IActionResult Delete(int id)
        {
            var values = postManager.TGetById(id);
            values.Status = false;
            postManager.TUpdate(values);

            if (values.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
            {
                // Delete Current Image
                string currentImage = values.Image;
                string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Post/", currentImage);
                System.IO.File.Delete(currentImagePath);
            }

            TempData["SuccessMessage"] = "Blog başarıyla silindi";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> categories = (from x in postCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            var values = postManager.TGetById(id);
            return View(values);
        }

        [HttpPost]
        public IActionResult Edit(Post post, IFormFile image)
        {
            PostValidator validator = new PostValidator();
            ValidationResult results = validator.Validate(post);

            if (results.IsValid)
            {
                var values = postManager.TGetById(post.Id);

                if (image != null && image.Length > 0)
                {
                    if (values.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
                    {
                        // Delete Current Image In Direction
                        string currentImage = values.Image;
                        string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Post/", currentImage);
                        System.IO.File.Delete(currentImagePath);
                    }

                    // Save New Image
                    var path = Path.GetExtension(image.FileName);
                    var guidFileName = Guid.NewGuid() + path;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Post/");
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

                values.Title = post.Title;
                values.PostCategoryId = post.PostCategoryId;
                values.Content = post.Content;

                postManager.TUpdate(values);

                TempData["SuccessMessage"] = "Blog başarıyla güncellendi";

                return RedirectToAction("Index");
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            List<SelectListItem> categories = (from x in postCategoryManager.TList().Where(x => x.Status == true).OrderBy(x => x.Name)
                                               select new SelectListItem
                                               {
                                                   Text = x.Name,
                                                   Value = x.Id.ToString()
                                               }).ToList();

            ViewBag.Categories = categories;

            return View(post);
        }

        public IActionResult Details(int id)
        {
            var values = postManager.GetPostWithCategory(id);
            return View(values);
        }

        public IActionResult Comments(int id, int page = 1)
        {
            var values = postCommentManager.GetCommentsByPost(id).ToPagedList(page, 10);
            ViewBag.PostTitle = postManager.TGetById(id).Title;
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedPosts)
        {
            foreach (var postId in selectedPosts)
            {
                var post = postManager.TGetById(postId);
                post.Status = false;
                postManager.TUpdate(post);

                if (post.Image != "/Templates/admin-template/assets/images/defaul-post-image.png")
                {
                    // Delete Current Image
                    string currentImage = post.Image;
                    string currentImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/Post/", currentImage);
                    System.IO.File.Delete(currentImagePath);
                }
            }

            TempData["SuccessMessage"] = "Seçilen bloglar başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
