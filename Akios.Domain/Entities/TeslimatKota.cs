using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class TeslimatKota
    {
        public int TeslimatKotaId { get; set; }
        public int MusteriId { get; set; }
        public DateTime TeslimatTarihi { get; set; }
        public int? MaxTeslimatSayisi { get; set; }
        public bool TeslimatYapilacakMi { get; set; }
    }
}
