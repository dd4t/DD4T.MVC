using DD4T.Core.Contracts.ViewModels;
using System.Web.Mvc;

namespace Tridion.Web.Controllers
{
    public class DD4TDefaultController : Controller
    {
        public ActionResult Page(IViewModel page, string view)
        {
            return View(view, page);
        }

        public ActionResult Component(IViewModel model, string view)
        {
            return View(view, model);
        }
    }
}