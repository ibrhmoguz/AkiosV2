﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Concrete;
using Akios.Domain.Entities;
using Akios.Domain.Interface;

namespace Akios.Domain.Repo
{
    public class KonfigurasyonRepo : IKonfigurasyonRepo
    {
        private EFDbContext context = new EFDbContext();
        public IEnumerable<Konfigurasyon> Konfigurasyonlar 
        {
            get { return context.Konfigurasyonlar.ToList(); }
        }
    }
}
