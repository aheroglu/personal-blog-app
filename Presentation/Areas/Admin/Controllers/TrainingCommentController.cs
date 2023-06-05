using Business.Concrete;
using DataAccess.EntityFramework;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrainingCommentController : Controller
    {
        TrainingCommentManager trainingCommentManager = new TrainingCommentManager(new EfTrainingCommentDal());

        public IActionResult Index(int page = 1)
        {
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }

            var values = trainingCommentManager.GetListWithTraining().ToPagedList(page, 10);
            return View(values);
        }

        public IActionResult Delete(int id)
        {
            var values = trainingCommentManager.TGetById(id);
            values.Status = false;
            trainingCommentManager.TUpdate(values);
            TempData["SuccessMessage"] = "Eğitim yorumu başarıyla silindi";
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var values = trainingCommentManager.GetCommentWithTraining(id);
            return View(values);
        }

        public IActionResult DeleteSelected(int[] selectedComments)
        {
            foreach (var commentId in selectedComments)
            {
                var comment = trainingCommentManager.TGetById(commentId);
                comment.Status = false;
                trainingCommentManager.TUpdate(comment);
            }

            TempData["SuccessMessage"] = "Seçilen eğitim yorumları başarıyla silindi";

            return RedirectToAction("Index");
        }

    }
}
