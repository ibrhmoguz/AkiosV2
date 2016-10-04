using Akios.Domain.Entities;
using System.Collections.Generic;

namespace Akios.Admin.Models
{
    public class KullaniciListViewModel
    {
        public IEnumerable<Kullanici> Kullanicilar { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public KullaniciYetkileri kullaniciYetkileri { get; set; }
    }
}