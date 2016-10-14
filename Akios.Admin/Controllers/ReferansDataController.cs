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
    public class ReferansDataController : Controller
    {
        private IRefDataRepo refDataRepo;
        private IMusteriRepo musteriRepo;

        public ReferansDataController(IRefDataRepo rr, IMusteriRepo mr)
        {
            refDataRepo = rr;
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

            var joinedList = from r in refDataRepo.ReferansDatalar
                             join m in musteriRepo.Musteriler on r.MusteriId equals m.MusteriId
                             select new Tuple<string, string, string>(
                                 m.Adi,
                                 r.RefAdi,
                                 r.RefId.ToString());

            if (!string.IsNullOrEmpty(iSearch))
            {
                var search = iSearch.ToLower();
                joinedList = joinedList.Where(x => x.Item1.ToLower().Contains(search) ||
                                                   x.Item2.ToLower().Contains(search));
            }

            var filteredList = joinedList.ToList();
            var totalRecords = filteredList.Count();

            if (iDisplayLength == -1)
            {
                iDisplayLength = totalRecords;
            }

            var list = filteredList.Skip(iDisplayStart).Take(iDisplayLength);

            Func<Tuple<string, string, string>, string> orderFunc = (item => iSortColumnIndex == 1 ? item.Item1 : item.Item2);
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
