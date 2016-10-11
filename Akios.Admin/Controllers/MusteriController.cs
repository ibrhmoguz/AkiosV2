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

        public string JsonList()
        {
            var iDisplayLength = int.Parse(Request["iDisplayLength"]);
            var iDisplayStart = int.Parse(Request["iDisplayStart"]);
            var iSearch = Request["sSearch"];
            var iSortColumnIndex = Convert.ToInt32(Request["iSortCol_0"]);
            var iSortDirection = Request["sSortDir_0"];

            var filteredList = musteriRepo.Musteriler;
            if (!string.IsNullOrEmpty(iSearch))
            {
                filteredList = musteriRepo.Musteriler.Where(x => x.Adi.Contains(iSearch) ||
                                                   x.Adres.Contains(iSearch) ||
                                                   x.Faks.Contains(iSearch) ||
                                                   x.Kod.Contains(iSearch) ||
                                                   x.Mail.Contains(iSearch) ||
                                                   x.Mobil.Contains(iSearch) ||
                                                   x.Tel.Contains(iSearch) ||
                                                   x.YetkiliKisi.Contains(iSearch));
            }

            Func<Musteri, string> orderFunc = (item => iSortColumnIndex == 1
                ? item.Kod
                : iSortColumnIndex == 2
                    ? item.YetkiliKisi
                    : iSortColumnIndex == 3
                        ? item.Adres
                        : iSortColumnIndex == 4
                            ? item.Tel
                            : iSortColumnIndex == 5
                                ? item.Mobil
                                : iSortColumnIndex == 6
                                    ? item.Mail
                                    : item.Web);

            var list = filteredList.Skip(iDisplayStart).Take(iDisplayLength);
            var orderedList = (iSortDirection == "asc") ? list.OrderBy(orderFunc).ToList() : list.OrderByDescending(orderFunc).ToList();

            var result = new
            {
                iTotalRecords = musteriRepo.Musteriler.Count(),
                iTotalDisplayRecords = orderedList.Count(),
                aaData = (from item in orderedList
                          select new[] 
                            {
                                Convert.ToString(item.MusteriId), 
                                item.Kod + "-" + item.Adi,
                                item.YetkiliKisi,
                                item.Adres,
                                item.Tel,
                                item.Faks,
                                item.Mobil,
                                item.Mail,
                                item.Web,
                                "",
                                ""
                            })
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}
