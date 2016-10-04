using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akios.Admin.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            if (Session["CurrentUserId"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
                return View();
        }
    }
}