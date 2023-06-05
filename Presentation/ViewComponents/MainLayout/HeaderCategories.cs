using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Presentation.ViewComponents.MainLayout
{
    public class HeaderCategories : ViewComponent
    {
        PostCategoryManager postCategoryManager = new PostCategoryManager(new EfPostCategoryDal());

        public IViewComponentResult Invoke()
        {
            var values = postCategoryManager.TList().Where(x => x.Status == true);
            return View(values);
        }
    }
}
