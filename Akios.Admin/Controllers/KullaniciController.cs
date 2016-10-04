using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akios.Admin.Infrastructure.Concrete;
using Akios.Admin.Models;
using Akios.Domain.Entities;
using Akios.Domain.Interface;

namespace Akios.Admin.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class KullaniciController : Controller
    {
        IKullaniciRepo kullaniciRepo;
        public int PageSize = 5;
        public KullaniciController(IKullaniciRepo kr)
        {
            kullaniciRepo = kr;
        }

        public ViewResult Liste(int page = 1)
        {
            var pagedKullanicilar = kullaniciRepo.Kullanicilar.OrderBy(p => p.KullaniciId).Skip((page - 1) * PageSize).Take(PageSize);
            var model = new KullaniciListViewModel()
            {
                Kullanicilar = pagedKullanicilar,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = kullaniciRepo.Kullanicilar.Count()
                },
                kullaniciYetkileri = Session["CurrentUser_Auths"] as KullaniciYetkileri
            };
            return View(model);
        }

        public FileContentResult GetFoto(int Id)
        {
            Kullanici k = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciId.Equals(Id));
            if (k != null)
            {
                return File(k.FotoData, k.FotoMimeType);
            }
            return null;
        }

        public ViewResult Ekle()
        {
            return View("Guncelle", new Kullanici());
        }

        [HttpPost]
        public ViewResult GuncelleView(int kullaniciId)
        {
            Kullanici kullanici = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciId == kullaniciId);
            return View("Guncelle", kullanici);
        }

        [HttpPost]
        public ActionResult Guncelle(Kullanici k, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    k.FotoMimeType = image.ContentType;
                    k.FotoData = new byte[image.ContentLength];
                    image.InputStream.Read(k.FotoData, 0, image.ContentLength);
                }
                kullaniciRepo.KullaniciKaydet(k);
                TempData["message"] = string.Format("Kullanıcı {0}  kayıt edildi.", k.KullaniciAdi);
                return RedirectToAction("Liste");
            }
            else
            {
                return View(k);
            }
        }

        [HttpPost]
        public ActionResult Sil(int Id)
        {
            var kullaniciAdi = kullaniciRepo.KullaniciSil(Id);
            if (!string.IsNullOrEmpty(kullaniciAdi))
            {
                TempData["message"] = string.Format("{0} silindi.", kullaniciAdi);
            }
            else
            {
                TempData["message"] = string.Format("{0} silinemedi!", kullaniciAdi);
            }

            return RedirectToAction("Liste");
        }

        public FileContentResult FotoYukle(int kullaniciId)
        {
            Kullanici k = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciId.Equals(kullaniciId));
            if (k != null && k.FotoData != null)
            {

                return File(k.FotoData, k.FotoMimeType);
            }
            return File(System.IO.File.ReadAllBytes(ControllerContext.HttpContext.Server.MapPath("~/Content/Image/userProfile.jpg")), "image/jpeg");
        }

        public FileContentResult SessionFotoYukle()
        {
            if (Session["CurrentUser_FotoData"] != null && Session["CurrentUser_FotoMimeType"] != null)
            {
                return File(Session["CurrentUser_FotoData"] as byte[], Session["CurrentUser_FotoMimeType"].ToString());
            }

            if (Session["CurrentUserId"] != null)
            {
                Kullanici k = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciId.ToString().Equals(Session["CurrentUserId"].ToString()));
                if (k != null && k.FotoData != null)
                {
                    Session["CurrentUser_FotoData"] = k.FotoData;
                    Session["CurrentUser_FotoMimeType"] = k.FotoMimeType;
                    return File(k.FotoData, k.FotoMimeType);
                }
            }

            return File(System.IO.File.ReadAllBytes(ControllerContext.HttpContext.Server.MapPath("~/Content/Image/userProfile.jpg")), "image/jpeg");
        }
    }
}