using System.Web.Mvc;

namespace DevStartPage.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Refresh()
        {

            return View("Index");
        }

        public JsonResult UpdateSession(int nthriveId)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}