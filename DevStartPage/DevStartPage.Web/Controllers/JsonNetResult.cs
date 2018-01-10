using System;
using System.Threading;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DevStartPage.Web.Controllers
{
	public class JsonNetResult : JsonResult
	{
		private static readonly Lazy<JsonSerializer> LazySerializer = new Lazy<JsonSerializer>(GetSerializer, LazyThreadSafetyMode.PublicationOnly);

		private static JsonSerializer GetSerializer()
		{
			var serializer = new JsonSerializer
			{
				ContractResolver = new DefaultContractResolver
				{
					IgnoreSerializableAttribute = true,
					IgnoreSerializableInterface = true
				}
			};
			serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
			serializer.Converters.Add(new IsoDateTimeConverter());

			return serializer;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			var response = context.HttpContext.Response;
			response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

			if (ContentEncoding != null)
			{
				response.ContentEncoding = ContentEncoding;
			}

			if (Data != null)
			{
				using (var writer = new JsonTextWriter(response.Output))
				{
					LazySerializer.Value.Serialize(writer, Data);
				}
			}
		}
	}
}