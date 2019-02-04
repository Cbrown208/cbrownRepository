using System.Web.Mvc;

namespace DevStartPage.Web.AngularCli.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
	        var userIpAddress = GetIpAddress();
	        ViewBag.Message = userIpAddress;
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

	    public string GetIpAddress()
	    {
		    System.Web.HttpContext context = System.Web.HttpContext.Current;
		    string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

		    if (!string.IsNullOrEmpty(ipAddress))
		    {
			    string[] addresses = ipAddress.Split(',');
			    if (addresses.Length != 0)
			    {
				    return addresses[0];
			    }
		    }

		    return context.Request.ServerVariables["REMOTE_ADDR"];
	    }
	}
}