using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ynnova.YTrac.Web.Infrastructure;

namespace Ynnova.YTrac.Web.Controllers
{
	public class HomeController : RavenController
	{
		public ActionResult Index()
		{
			var session = RavenSession;

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
	}
}