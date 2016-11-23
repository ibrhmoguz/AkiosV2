using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class RefDetaySiparisSeri
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        public int RefDetaySiparisSeriId { get; set; }
        public int SiparisSeriId { get; set; }
        public int RefDetayId { get; set; }
    }
}
