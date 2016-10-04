using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class GrupKullanici
    {
        public int GrupKullaniciId { get; set; }
        public int GrupId { get; set; }
        public int KullaniciId { get; set; }
    }
}
