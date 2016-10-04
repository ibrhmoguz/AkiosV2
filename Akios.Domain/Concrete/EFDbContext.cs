using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Concrete
{
    public class EFDbContext : DbContext
    {
        public EFDbContext()
            : base("akiosAppConnectionString")
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
        public DbSet<Konfigurasyon> Konfigurasyonlar { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<RefDataDetay> ReferansDataDetaylar { get; set; }
        public DbSet<RefData> ReferansDatalar { get; set; }
        public DbSet<RefDetaySiparisSeri> RefDetaySiparisSeriler { get; set; }
        public DbSet<Sayac> Sayaclar { get; set; }
        public DbSet<SiparisSeri> SiparisSeriler { get; set; }
        public DbSet<TeslimatKota> TeslimatKotalar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
