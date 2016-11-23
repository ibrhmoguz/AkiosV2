using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akios.Admin.Infrastructure.Concrete;

namespace Akios.Admin.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class KontrolPaneliController : Controller
    {
        //
        // GET: /KontrolPaneli/

        public ActionResult Liste()
        {
            return View();
        }

    }
}
