using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Grup
    {
        public int GrupId { get; set; }
        public string GrupAdi { get; set; }
        public int? MusteriId { get; set; }
    }
}
