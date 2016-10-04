using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Konfigurasyon
    {
        public int KonfigurasyonId { get; set; }
        public int MusteriId { get; set; }
        public string KonfigAdi { get; set; }
        public string KonfigDegeri { get; set; }
    }
}
