using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class SiparisSeri
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int? SiparisSeriId { get; set; }
        public int MusteriId { get; set; }
        public string SeriAdi { get; set; }
        public string SeriKodu { get; set; }
    }
}
