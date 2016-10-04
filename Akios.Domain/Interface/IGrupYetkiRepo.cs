using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Interface
{
    public interface IGrupYetkiRepo
    {
        IEnumerable<GrupYetki> GrupYetkiler { get; }
        void GrupYetkiKaydet(GrupYetki g);
        string GrupYetkiSilGrupIdIle(int grupId);
        bool GrupYetkiSilYetkiIdVeGrupIdIle(int yetkiId, int grupId);
    }
}
