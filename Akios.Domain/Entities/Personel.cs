using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Personel
    {
        public int PersonelId { get; set; }
        public int MusteriId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
    }
}
