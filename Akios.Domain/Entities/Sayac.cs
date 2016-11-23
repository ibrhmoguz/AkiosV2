using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class Sayac
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SayacId { get; set; }
        public int Yil { get; set; }
        public int SiparisSeriId { get; set; }
        public int SiraNo { get; set; }
    }
}
