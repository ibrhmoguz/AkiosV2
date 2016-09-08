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
    public class GrupYetkiRepo : IGrupYetkiRepo
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<GrupYetki> GrupYetkiler
        {
            get
            {
                return context.GrupYetkiler.ToList();
            }
        }

        public void GrupYetkiKaydet(GrupYetki g)
        {
            var grupYetki = context.GrupYetkiler.Where(p => p.GrupId.Equals(g.GrupId) && p.YetkiId.Equals(g.YetkiId)).FirstOrDefault();
            if (grupYetki == null)
            {
                context.GrupYetkiler.Add(g);
            }
            else
            {
                grupYetki.GrupId = g.GrupId;
                grupYetki.YetkiId = g.YetkiId;
            }

            context.SaveChanges();
        }

        public string GrupYetkiSilGrupIdIle(int grupId)
        {
            throw new NotImplementedException();
        }

        public bool GrupYetkiSilYetkiIdVeGrupIdIle(int yetkiId, int grupId)
        {
            var grupYetki = context.GrupYetkiler.Where(p => p.GrupId.Equals(grupId) && p.YetkiId.Equals(yetkiId)).FirstOrDefault();
            if (grupYetki != null)
            {
                context.GrupYetkiler.Remove(grupYetki);
                context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
