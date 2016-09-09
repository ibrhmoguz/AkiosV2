using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public int? MusteriId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public byte[] FotoData { get; set; }
        public string FotoMimeType { get; set; }
        public bool IsAdmin { get; set; }
    }
}
