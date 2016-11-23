using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Akios.Admin.Infrastructure.Concrete;
using Akios.Domain.Interface;
using Newtonsoft.Json;

namespace Akios.Admin.Controllers
{
    [Authorize]
    [SessionExpireFilter]
    public class PersonelController : Controller
    {
        private IPersonelRepo personelRepo;
        private IMusteriRepo musteriRepo;

        public PersonelController(IPersonelRepo pr, IMusteriRepo mr)
        {
            personelRepo = pr;
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
            var totalRecords = personelRepo.Personeller.Count();

            if (iDisplayLength == -1)
            {
                iDisplayLength = totalRecords;
            }

            var joinedList = from p in personelRepo.Personeller
                             join m in musteriRepo.Musteriler on p.MusteriId equals m.MusteriId
                             select new Tuple<string, string, string, string>(
                                 p.Ad,
                                 p.Soyad,
                                 m.Adi,
                                 p.PersonelId.ToString());

            if (!string.IsNullOrEmpty(iSearch))
            {
                var search = iSearch.ToLower();
                joinedList = joinedList.Where(x => x.Item1.ToLower().Contains(search) ||
                                                   x.Item2.ToLower().Contains(search) ||
                                                   x.Item3.ToLower().Contains(search));
            }

            var filteredList = joinedList.ToList();
            Func<Tuple<string, string, string, string>, string> orderFunc = (item => iSortColumnIndex == 1
                ? item.Item1
                : iSortColumnIndex == 2
                    ? item.Item2
                        : item.Item3);

            var orderedList = (iSortDirection == "asc") ? filteredList.OrderBy(orderFunc).ToList() : filteredList.OrderByDescending(orderFunc).ToList();
            var list = orderedList.Skip(iDisplayStart).Take(iDisplayLength);

            var result = new
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = filteredList.Count,
                aaData = (from item in list
                          select new[] 
                            {
                                item.Item4,
                                item.Item1,
                                item.Item2,
                                item.Item3,
                                "",
                                ""
                            })
            };

            return JsonConvert.SerializeObject(result);
        }

    }
}
