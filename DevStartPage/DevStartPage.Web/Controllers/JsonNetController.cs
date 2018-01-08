using System.Text;
using System.Web.Mvc;
using MedAssets.Web.Mvc.ActionResults;
using NLog;
using StructureMap;

namespace DevStartPage.Web.Controllers
{
    public abstract class JsonNetController : Controller
    {
        protected readonly ILogger _logger;
        public JsonNetController()
        {
            _logger = ObjectFactory.GetInstance<ILogger>();
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            var result = new JsonNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
            return result;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            filterContext.Result = new JsonResult
            {
                Data = new
                {
                    Success = false,
                    Message = filterContext.Exception.Message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            filterContext.ExceptionHandled = true;
        }
    }
}