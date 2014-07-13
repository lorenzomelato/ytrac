using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ynnova.YTrac.Web.Infrastructure
{
	public class RavenController : Controller
	{
		public IAsyncDocumentSession RavenSession { get; set; } 
	}
}