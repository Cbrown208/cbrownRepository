using System.Web.Mvc;

namespace TestingSite.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var userIpAddress = GetIPAddress();
			ViewBag.Message = userIpAddress;
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		public ActionResult Downloads()
		{
			ViewBag.Message = "Downloads";

			return View();
		}

		protected string GetIPAddress()
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