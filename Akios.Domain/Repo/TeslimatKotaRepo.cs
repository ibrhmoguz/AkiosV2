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
    public class TeslimatKotaRepo : ITeslimatKotaRepo
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<TeslimatKota> TeslimatKotalar
        {
            get { return context.TeslimatKotalar.ToList(); }
        }
    }
}
