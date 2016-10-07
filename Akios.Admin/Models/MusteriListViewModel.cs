using Akios.Domain.Entities;
using System.Collections.Generic;

namespace Akios.Admin.Models
{
    public class MusteriListViewModel
    {
        public IEnumerable<Musteri> Musteriler { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public KullaniciYetkileri kullaniciYetkileri { get; set; }
    }
}