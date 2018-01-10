using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DevStartPage.Web.Controllers
{
	public class ServicesController : JsonNetController
	{

		[OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
		public JsonResult GetIpAddress()
		{
			string response;
			HttpContext context = System.Web.HttpContext.Current;
			string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

			if (!string.IsNullOrEmpty(ipAddress))
			{
				string[] addresses = ipAddress.Split(',');
				if (addresses.Length != 0)
				{
					response = addresses[0];
					return Json(response, JsonRequestBehavior.AllowGet);
				}
			}
			response = context.Request.ServerVariables["REMOTE_ADDR"];
			return Json(response, JsonRequestBehavior.AllowGet);
		}
	}
}