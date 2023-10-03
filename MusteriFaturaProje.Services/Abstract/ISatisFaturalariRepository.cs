using MusteriFaturaProje.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusteriFaturaProje.Services.Abstract
{
    public interface ISatisFaturalariRepository<T> : IGenericRepository<T> where T : class
    {
        List<SatisFaturasi> GetAll(bool satirlarDahilMi);
    }
}
