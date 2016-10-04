using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Interface
{
    public interface IGrupRepo
    {
        IEnumerable<Grup> Gruplar { get; }
        void GrupKaydet(Grup g);
        string GrupSil(int Id);
        IEnumerable<Kullanici> GrupKullanicilariniGetir(int grupId);
        IEnumerable<Yetki> GrupYetkileriniGetir(int grupId);
        IEnumerable<string> KullaniciYetkileri(int kullaniciId);
    }
}
