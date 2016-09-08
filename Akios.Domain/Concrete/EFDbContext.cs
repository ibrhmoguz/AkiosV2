using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("akiosConnectionString")
        {

        }

        public EFDbContext(string connectionString)
            : base(connectionString)
        {

        }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Grup> Gruplar { get; set; }
        public DbSet<Yetki> Yetkiler { get; set; }
        public DbSet<GrupKullanici> GrupKullanicilar { get; set; }
        public DbSet<GrupYetki> GrupYetkiler { get; set; }
    }
}
