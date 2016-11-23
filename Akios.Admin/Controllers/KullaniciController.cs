using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akios.Admin.Infrastructure.Concrete;
using Akios.Admin.Models;
using Akios.Domain.Entities;
using Akios.Domain.Interface;
using Newtonsoft.Json;

namespace Akios.Admin.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class KullaniciController : Controller
    {
        private IKullaniciRepo kullaniciRepo;
        private IMusteriRepo musteriRepo;
        public KullaniciController(IKullaniciRepo kr, IMusteriRepo mr)
        {
            kullaniciRepo = kr;
            musteriRepo = mr;
        }

        public ViewResult Liste()
        {
            return View();
        }

        public string JsonList()
        {
            var iDisplayLength = int.Parse(Request["iDisplayLength"]);
            var iDisplayStart = int.Parse(Request["iDisplayStart"]);
            var iSearch = Request["sSearch"];
            var iSortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var iSortDirection = Request["sSortDir_0"];
            var totalRecords = kullaniciRepo.Kullanicilar.Count();

            if (iDisplayLength == -1)
            {
                iDisplayLength = totalRecords;
            }

            var joinedList = from k in kullaniciRepo.Kullanicilar
                             join m in musteriRepo.Musteriler on k.MusteriId equals m.MusteriId into jList
                             from jm in jList.DefaultIfEmpty()
                             select new Tuple<string, string, string, string, string>(
                                 k.KullaniciAdi,
                                 k.Adi,
                                 k.Soyadi,
                                 (jm == null) ? string.Empty : jm.Adi,
                                 k.KullaniciId.ToString());

            if (!string.IsNullOrEmpty(iSearch))
            {
                var search = iSearch.ToLower();
                joinedList = joinedList.Where(x => x.Item1.ToLower().Contains(search) ||
                                                   x.Item2.ToLower().Contains(search) ||
                                                   x.Item3.ToLower().Contains(search) ||
                                                   x.Item4.ToLower().Contains(search));
            }

            var filteredList = joinedList.ToList();
            Func<Tuple<string, string, string, string, string>, string> orderFunc = (item => iSortColumnIndex == 1
                ? item.Item1
                : iSortColumnIndex == 2
                    ? item.Item2
                    : iSortColumnIndex == 3
                        ? item.Item3
                        : item.Item4);

            var orderedList = (iSortDirection == "asc") ? filteredList.OrderBy(orderFunc).ToList() : filteredList.OrderByDescending(orderFunc).ToList();
            var list = orderedList.Skip(iDisplayStart).Take(iDisplayLength);

            var result = new
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = filteredList.Count,
                aaData = (from item in list
                          select new[] 
                            {
                                item.Item5, 
                                item.Item1,
                                item.Item2,
                                item.Item3,
                                item.Item4,
                                "",
                                ""
                            })
            };

            return JsonConvert.SerializeObject(result);
        }

        public ViewResult Ekle()
        {
            return View("Kaydet", new Kullanici());
        }

        [HttpPost]
        public ViewResult KaydetView(int kullaniciId)
        {
            Kullanici kullanici = kullaniciRepo.Kullanicilar.FirstOrDefault(x => x.KullaniciId == kullaniciId);
            return View("Kaydet", kullanici);
        }

        [HttpPost]
        public ActionResult Kaydet(Kullanici k, HttpPostedFileBase image = null)
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

        [HttpPost]
        public string FotoUpload()
        {

            var result = new { Data = "success" };
            return JsonConvert.SerializeObject(result);
        }
    }
}