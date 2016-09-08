using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Interface
{
    public interface IGrupKullaniciRepo
    {
        IEnumerable<GrupKullanici> GrupKullanicilar { get; }
        void GrupKullaniciKaydet(GrupKullanici g);
        string GrupKullaniciSilGrupIdIle(int grupId);
        bool GrupKullaniciSilKullaniciIdVeGrupIdIle(int kullaniciId, int grupId);
    }
}
