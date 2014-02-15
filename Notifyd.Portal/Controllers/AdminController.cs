using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notifyd.Portal.CustomAttributes;

namespace Notifyd.Portal.Controllers
{
    [NotifydAuthAttribute]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
	}
}