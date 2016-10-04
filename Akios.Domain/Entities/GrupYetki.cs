using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class GrupYetki
    {
        public int GrupYetkiId { get; set; }
        public int GrupId { get; set; }
        public int YetkiId { get; set; }
    }
}
