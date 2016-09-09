﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akios.Domain.Entities
{
    public class RefData
    {
        [Key]
        public int RefId { get; set; }
        public int MusteriId { get; set; }
        public string RefAdi { get; set; }
    }
}
