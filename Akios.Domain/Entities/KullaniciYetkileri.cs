using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Akios.Domain.Entities
{
    public class KullaniciYetkileri
    {
        public bool KullaniciEkleme { get; private set; }
        public bool KullaniciSilme { get; private set; }
        public bool KullaniciDegistirme { get; private set; }
        public bool KullaniciListeleme { get; private set; }
        public bool PersonelEkleme { get; private set; }
        public bool PersonelSilme { get; private set; }
        public bool PersonelDegistirme { get; private set; }
        public bool PersonelListeleme { get; private set; }
        public bool GrupIzinAyarlama { get; private set; }

        public KullaniciYetkileri(IEnumerable<string> yetkiListesi)
        {
            foreach (var yetki in yetkiListesi)
            {
                switch (yetki)
                {
                    case "Kullanıcı Ekleme":
                        this.KullaniciEkleme = true;
                        break;
                    case "Kullanıcı Silme":
                        this.KullaniciSilme = true;
                        break;
                    case "Kullanıcı Değiştirme":
                        this.KullaniciDegistirme = true;
                        break;
                    case "Kullanıcı Listeleme":
                        this.KullaniciListeleme = true;
                        break;
                    case "Personel Ekleme":
                        this.PersonelEkleme = true;
                        break;
                    case "Personel Silme":
                        this.PersonelSilme = true;
                        break;
                    case "Personel Değiştirme":
                        this.PersonelDegistirme = true;
                        break;
                    case "Personel Listeleme":
                        this.PersonelListeleme = true;
                        break;
                    case "Grup ve İzin ayarlama":
                        this.GrupIzinAyarlama = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}