using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace DevStartPage.Web.Controllers
{
    public class ServicesController : JsonNetController
    {

        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public async Task<JsonResult> GetFacilityConfigurationBynThriveId()
        {
            var apiUrl = $"FacilityConfigAuthorized/GetConfig?nThriveId=";
            var response = "";
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public async Task<JsonResult> GetEnabledFacilityList(string clientId)
        {
            var apiUrl = $"FacilityConfigAuthorized/GetPfeEnabledFacilityList?clientId={clientId}";
	        var response = "";
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}