using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Concrete;
using Akios.Domain.Entities;
using Akios.Domain.Interface;

namespace Akios.Domain.Repo
{
    public class KullaniciRepo : IKullaniciRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Kullanici> Kullanicilar
        {
            get { return context.Kullanicilar.ToList(); }
        }

        public void KullaniciKaydet(Kullanici k)
        {
            k.KayitTarihi = DateTime.Now;

            if (k.KullaniciId == 0)
            {
                context.Kullanicilar.Add(k);
            }
            else
            {
                Kullanici kc = context.Kullanicilar.Find(k.KullaniciId);
                if (kc != null)
                {
                    kc.KullaniciAdi = k.KullaniciAdi;
                    kc.Sifre = k.Sifre;
                    kc.Adi = k.Adi;
                    kc.Soyadi = k.Soyadi;
                    if (k.FotoData != null)
                    {
                        kc.FotoData = k.FotoData;
                        kc.FotoMimeType = k.FotoMimeType;
                    }
                    kc.KayitTarihi = k.KayitTarihi;
                }
            }
            context.SaveChanges();
        }

        public string KullaniciSil(int Id)
        {
            Kullanici k = context.Kullanicilar.Find(Id);
            if (k != null)
            {
                context.Kullanicilar.Remove(k);
                context.SaveChanges();
                return k.KullaniciAdi;
            }

            return string.Empty;
        }
    }
}
