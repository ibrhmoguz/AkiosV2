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
    public class GrupRepo : IGrupRepo
    {
        IKullaniciRepo kullaniciRepo;
        IYetkiRepo yetkiRepo;
        IGrupKullaniciRepo gkRepo;
        IGrupYetkiRepo giRepo;
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Grup> Gruplar
        {
            get { return context.Gruplar.ToList(); }
        }

        public GrupRepo(IKullaniciRepo krepo, IYetkiRepo irepo, IGrupKullaniciRepo gkr, IGrupYetkiRepo gir)
        {
            this.kullaniciRepo = krepo;
            this.yetkiRepo = irepo;
            this.gkRepo = gkr;
            this.giRepo = gir;
        }

        public void GrupKaydet(Grup g)
        {
            if (g.GrupId == 0)
            {
                context.Gruplar.Add(g);
            }
            else
            {
                Grup gp = context.Gruplar.Find(g.GrupId);
                if (gp != null)
                {
                    gp.GrupAdi = g.GrupAdi;
                }
            }
            context.SaveChanges();
        }

        public string GrupSil(int Id)
        {
            Grup g = context.Gruplar.Find(Id);
            if (g != null)
            {
                var yetkiler = context.GrupYetkiler.Where(p => p.GrupId.Equals(Id));
                foreach (var yetki in yetkiler)
                {
                    context.GrupYetkiler.Remove(yetki);
                }

                var kullanicilar = context.GrupKullanicilar.Where(p => p.GrupId.Equals(Id));
                foreach (var kullanici in kullanicilar)
                {
                    context.GrupKullanicilar.Remove(kullanici);
                }

                context.Gruplar.Remove(g);
                context.SaveChanges();
                return g.GrupId.ToString();
            }

            return string.Empty;
        }

        public IEnumerable<Kullanici> GrupKullanicilariniGetir(int grupId)
        {
            return (from g in this.Gruplar
                    join gk in gkRepo.GrupKullanicilar on g.GrupId equals gk.GrupId
                    join k in kullaniciRepo.Kullanicilar on gk.KullaniciId equals k.KullaniciId
                    where g.GrupId.Equals(grupId)
                    select k).ToList();
        }

        public IEnumerable<Yetki> GrupYetkileriniGetir(int grupId)
        {
            return (from g in this.Gruplar
                    join gy in giRepo.GrupYetkiler on g.GrupId equals gy.GrupId
                    join i in yetkiRepo.Yetkiler on gy.YetkiId equals i.YetkiId
                    where g.GrupId.Equals(grupId)
                    select i).ToList();
        }

        public IEnumerable<string> KullaniciYetkileri(int kullaniciId)
        {
            return (from k in kullaniciRepo.Kullanicilar
                    join gk in gkRepo.GrupKullanicilar on k.KullaniciId equals gk.KullaniciId
                    join gi in giRepo.GrupYetkiler on gk.GrupId equals gi.GrupId
                    join i in yetkiRepo.Yetkiler on gi.YetkiId equals i.YetkiId
                    where k.KullaniciId.Equals(kullaniciId)
                    select i.YetkiAdi).ToList();
        }
    }
}
