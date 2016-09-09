using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akios.Domain.Entities;

namespace Akios.Domain.Interface
{
    public interface ITeslimatKotaRepo
    {
        IEnumerable<TeslimatKota> TeslimatKotalar { get; }
    }
}
