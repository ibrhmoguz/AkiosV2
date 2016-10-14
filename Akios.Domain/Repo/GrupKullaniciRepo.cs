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
    public class GrupKullaniciRepo : IGrupKullaniciRepo
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<GrupKullanici> GrupKullanicilar
        {
            get
            {
                return context.GrupKullanicilar.ToList();
            }
        }

        public void GrupKullaniciKaydet(GrupKullanici g)
        {
            var grupKullanici = context.GrupKullanicilar.Where(p => p.GrupId.Equals(g.GrupId) && p.KullaniciId.Equals(g.KullaniciId)).FirstOrDefault();
            if (grupKullanici == null)
            {
                context.GrupKullanicilar.Add(g);
            }
            else
            {
                grupKullanici.GrupId = g.GrupId;
                grupKullanici.KullaniciId = g.KullaniciId;
            }

            context.SaveChanges();
        }

        public string GrupKullaniciSilGrupIdIle(int grupId)
        {
            throw new NotImplementedException();
        }

        public bool GrupKullaniciSilKullaniciIdVeGrupIdIle(int kullaniciId, int grupId)
        {
            var grupKullanici = context.GrupKullanicilar.Where(p => p.GrupId.Equals(grupId) && p.KullaniciId.Equals(kullaniciId)).FirstOrDefault();
            if (grupKullanici != null)
            {
                context.GrupKullanicilar.Remove(grupKullanici);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
