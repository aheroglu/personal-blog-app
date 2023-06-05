using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostCommentController : Controller
    {
        PostCommentManager postCommentManager = new PostCommentManager(new EfPostCommentDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = postCommentManager.GetListWithPost().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var values = postCommentManager.TGetById(id);
            values.Status = false;
            postCommentManager.TUpdate(values);
            TempData["SuccessMessage"] = "Yorum başarıyla silindi";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var values = postCommentManager.GetCommentWithPost(id);
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedComments)
        {
            foreach (var commentId in selectedComments)
            {
                var comment = postCommentManager.TGetById(commentId);
                comment.Status = false;
                postCommentManager.TUpdate(comment);
            }

            TempData["SuccessMessage"] = "Seçilen yorumlar başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
