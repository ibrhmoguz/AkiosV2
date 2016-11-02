using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Kullanici
    {
        public int KullaniciId { get; set; }
        [DisplayName("Kullanıcı Adı")]
        [Required]
        public string KullaniciAdi { get; set; }
        [DisplayName("Şifre")]
        [Required]
        public string Sifre { get; set; }
        [DisplayName("Adı")]
        [Required]
        public string Adi { get; set; }
        [DisplayName("Soyadı")]
        [Required]
        public string Soyadi { get; set; }
        public int? MusteriId { get; set; }
        public DateTime KayitTarihi { get; set; }
        public byte[] FotoData { get; set; }
        public string FotoMimeType { get; set; }
        public bool IsAdmin { get; set; }
    }
}
