using Business.Concrete;
using DataAccess.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.ViewComponents.MainLayout
{
    public class FooterAccounts : ViewComponent
    {
        AccountManager accountManager = new AccountManager(new EfAccountDal());

        public IViewComponentResult Invoke()
        {
            var values = accountManager.TList();
            return View(values);
        }
    }
}
