using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class RefDataDetay
    {
        [Key]
        public int RefDetayId { get; set; }
        public int RefId { get; set; }
        public string RefDetayAdi { get; set; }
    }
}
