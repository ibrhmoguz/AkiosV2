using Akios.Admin.Infrastructure.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Akios.Admin.Controllers
{
    [SessionExpireFilter]
    public class NavController : Controller
    {
        public PartialViewResult Menu()
        {
            if (Session["CurrentUser_Auths"] != null)
            {
                return PartialView(Session["CurrentUser_Auths"]);
            }

            return null;
        }
    }
}