using System.Web.Mvc;
using System.Web.UI;
using DevStartPage.Core.Managers;

namespace DevStartPage.Web.AngularCli.Controllers
{
	public class DownloadsController : JsonNetController
	{
		private readonly DownloadsManager _manager = new DownloadsManager();

		[OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
		public JsonResult GetFileList()
		{
			var response = _manager.GetFileList();
			return Json(response, JsonRequestBehavior.AllowGet);
		}
	}
}