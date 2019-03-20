using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WorkFinderAPI.Controllers
{
    public class TemplateController : Controller
    {
		private static Dictionary<string, string> routeDictionary;
		static TemplateController()
		{
			routeDictionary = new Dictionary<string, string>();
			routeDictionary["/"] = "test.js";
		}

		[Route("/")]
		// GET: Template
		public ActionResult Index()
        {
			ViewData["javaScript"] = routeDictionary[HttpContext.Request.Path.ToString()];
			return View();
        }
    }
}