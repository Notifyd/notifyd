using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notifyd.Portal.CustomAttributes;

namespace Notifyd.Portal.Controllers
{
    [NotifydAuthAttribute]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }
	}
}