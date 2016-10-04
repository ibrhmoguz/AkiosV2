using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Interface
{
    public interface IKullaniciRepo
    {
        IEnumerable<Kullanici> Kullanicilar { get; }
        void KullaniciKaydet(Kullanici k);
        string KullaniciSil(int Id);
    }
}
