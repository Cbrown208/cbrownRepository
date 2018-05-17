using System.Web.Mvc;
using MvcExercise.Core.Models;
using MvcExercise.Infrastructure.Interfaces;

namespace MvcExercise.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly IContactManager _contactManager;

		public HomeController(IContactManager contactManager)
		{
			_contactManager = contactManager;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult AddContact(Contact contactInfo)
		{
			_contactManager.AddContact(contactInfo);
			ModelState.Clear();

			return View("Index");
		}
	}
}