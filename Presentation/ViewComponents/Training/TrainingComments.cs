using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.Training
{
    public class TrainingComments : ViewComponent
    {
        TrainingCommentManager trainingCommentManager = new TrainingCommentManager(new EfTrainingCommentDal());

        public IViewComponentResult Invoke()
        {
            int trainingId = ViewBag.TrainingId;
            var values = trainingCommentManager.GetCommentsByPost(trainingId);
            ViewBag.CommentCount = trainingCommentManager.GetCommentsByPost(trainingId).Count;
            return View(values);
        }
    }
}
