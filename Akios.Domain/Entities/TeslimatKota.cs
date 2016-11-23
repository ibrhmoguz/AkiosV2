using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class TeslimatKota
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TeslimatKotaId { get; set; }
        public int MusteriId { get; set; }
        public DateTime TeslimatTarihi { get; set; }
        public int? MaxTeslimatSayisi { get; set; }
        public bool TeslimatYapilacakMi { get; set; }
    }
}
