﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Kullanici
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int KullaniciId { get; set; }

        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAdi { get; set; }

        [DisplayName("Şifre")]
        public string Sifre { get; set; }

        [DisplayName("Adı")]
        public string Adi { get; set; }

        [DisplayName("Soyadı")]
        public string Soyadi { get; set; }

        public int? MusteriId { get; set; }

        public DateTime KayitTarihi { get; set; }

        public byte[] FotoData { get; set; }

        public string FotoMimeType { get; set; }

        public bool IsAdmin { get; set; }
    }
}
