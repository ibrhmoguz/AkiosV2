using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akios.Admin.Infrastructure.Abstract;
using Akios.Domain.Interface;
using Akios.Admin.Models;
using Akios.Domain.Entities;

namespace Akios.Admin.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider authProvider;
        IKullaniciRepo kullaniciRepo;
        IGrupRepo grupRepo;
        public AccountController(IAuthProvider auth, IKullaniciRepo kr, IGrupRepo grupRepo)
        {
            this.authProvider = auth;
            this.kullaniciRepo = kr;
            this.grupRepo = grupRepo;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var kullanici = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi.Equals(model.KullaniciAdi) && x.Sifre.Equals(model.Sifre) && x.IsAdmin.Equals(true));
                if (kullanici != null)
                {
                    authProvider.Authenticate(model.KullaniciAdi, model.Sifre);
                    Session["CurrentUserName"] = kullanici.KullaniciAdi;
                    Session["CurrentUserName_SurName"] = kullanici.Adi + " " + kullanici.Soyadi;
                    Session["CurrentUserId"] = kullanici.KullaniciId;
                    Session["CurrentUser_Auths"] = new KullaniciYetkileri(grupRepo.KullaniciYetkileri(kullanici.KullaniciId));
                    return Redirect(returnUrl ?? Url.Action("Index", "Default"));
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya parola hatalı!");
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            Session.Clear();
            authProvider.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
    }
}
