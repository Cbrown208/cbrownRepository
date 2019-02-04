using System.Text;
using System.Web.Mvc;

namespace DevStartPage.Web.Controllers
{
    public abstract class JsonNetController : Controller
    {
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
	                filterContext.Exception.Message
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            filterContext.ExceptionHandled = true;
        }
    }
}