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
    public class GrupController : Controller
    {
        private IGrupRepo grupRepo;
        private IMusteriRepo musteriRepo;

        public GrupController(IGrupRepo gr, IMusteriRepo mr)
        {
            grupRepo = gr;
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

            var joinedList = from g in grupRepo.Gruplar
                             join m in musteriRepo.Musteriler on g.MusteriId equals m.MusteriId
                             select new Tuple<string, string, string>(
                                 g.GrupAdi,
                                 m.Adi,
                                 g.GrupId.ToString());

            if (!string.IsNullOrEmpty(iSearch))
            {
                var search = iSearch.ToLower();
                joinedList = joinedList.Where(x => x.Item1.ToLower().Contains(search) ||
                                                   x.Item2.ToLower().Contains(search) ||
                                                   x.Item3.ToLower().Contains(search));
            }

            var filteredList = joinedList.ToList();
            var totalRecords = filteredList.Count();

            if (iDisplayLength == -1)
            {
                iDisplayLength = totalRecords;
            }

            var list = filteredList.Skip(iDisplayStart).Take(iDisplayLength);

            Func<Tuple<string, string, string>, string> orderFunc = (item => iSortColumnIndex == 1
                ? item.Item1
                : iSortColumnIndex == 2
                    ? item.Item2
                        : item.Item3);

            var orderedList = (iSortDirection == "asc") ? list.OrderBy(orderFunc).ToList() : list.OrderByDescending(orderFunc).ToList();

            var result = new
            {
                iTotalRecords = totalRecords,
                iTotalDisplayRecords = totalRecords,
                aaData = (from item in orderedList
                          select new[] 
                            {
                                item.Item3,
                                item.Item1,
                                item.Item2,
                                "",
                                ""
                            })
            };

            return JsonConvert.SerializeObject(result);
        }

    }
}
