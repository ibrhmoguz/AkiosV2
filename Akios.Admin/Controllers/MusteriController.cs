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
    public class MusteriController : Controller
    {
        private IMusteriRepo musteriRepo;
        public int PageSize = 5;
        public MusteriController(IMusteriRepo mr)
        {
            musteriRepo = mr;
        }
        public ViewResult Liste(int page = 1)
        {
            //var pagedMusteriler = musteriRepo.Musteriler.OrderBy(p => p.MusteriId).Skip((page - 1) * PageSize).Take(PageSize);
            var model = new MusteriListViewModel()
            {
                Musteriler = null,
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = musteriRepo.Musteriler.Count()
                },
                kullaniciYetkileri = Session["CurrentUser_Auths"] as KullaniciYetkileri
            };
            return View(model);
        }

        public FileContentResult LogoYukle(int musteriId)
        {
            Musteri m = musteriRepo.Musteriler.FirstOrDefault(x => x.MusteriId.Equals(musteriId));
            if (m != null && m.LogoData != null)
            {
                return File(m.LogoData, m.LogoMimeType);
            }
            return File(System.IO.File.ReadAllBytes(ControllerContext.HttpContext.Server.MapPath("~/Content/Image/userProfile.jpg")), "image/jpeg");
        }

        public JsonResult JsonList()
        {
            var iDisplayLength = int.Parse(Request["iDisplayLength"]);
            var iDisplayStart = int.Parse(Request["iDisplayStart"]);

            var result = new
            {
                iTotalRecords = musteriRepo.Musteriler.Count(),
                iTotalDisplayRecords = musteriRepo.Musteriler.Count(),
                aaData = (from m in musteriRepo.Musteriler
                          select new
                          {
                              m.MusteriId,
                              KodMusteri = m.Kod + "-" + m.Adi,
                              m.YetkiliKisi,
                              m.Adres,
                              m.Tel,
                              m.Faks,
                              m.Mobil,
                              m.Mail,
                              m.Web
                          }).ToList().Skip(iDisplayStart).Take(iDisplayLength)
            };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
