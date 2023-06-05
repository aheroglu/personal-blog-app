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
    public class PostController : Controller
    {
        PostManager postManager = new PostManager(new EfPostDal());
        PostCommentManager postCommentManager = new PostCommentManager(new EfPostCommentDal());

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            ViewBag.PostId = id;
            postId = id;

            var post = postManager.TGetById(id);
            post.ClickCount = post.ClickCount + 1;
            postManager.TUpdate(post);

            return View();
        }

        public static int postId;

        [HttpPost]
        public IActionResult Details(PostComment postComment)
        {
            PostCommentValidator validator = new PostCommentValidator();
            ValidationResult results = validator.Validate(postComment);

            if (results.IsValid)
            {
                postComment.Id = 0;
                postComment.PostId = postId;
                postComment.Date = DateTime.Now;
                postComment.Status = true;

                postCommentManager.TInsert(postComment);

                TempData["SuccessMessage"] = "Yorumunuz başarıyla kaydedildi";

                return RedirectToAction("Details", new { id = postComment.PostId });
            }

            else
            {
                foreach (var x in results.Errors)
                {
                    ModelState.AddModelError(x.PropertyName, x.ErrorMessage);
                }
            }

            ViewBag.PostId = postId;

            return View(postComment);
        }

    }
}
