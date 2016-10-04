using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class SiparisSeri
    {
        public int? SiparisSeriId { get; set; }
        public int MusteriId { get; set; }
        public string SeriAdi { get; set; }
        public string SeriKodu { get; set; }
    }
}
