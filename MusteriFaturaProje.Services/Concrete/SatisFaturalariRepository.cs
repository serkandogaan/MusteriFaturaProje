using Microsoft.EntityFrameworkCore;
using MusteriFaturaProje.Entity.Context;
using MusteriFaturaProje.Entity.Models;
using MusteriFaturaProje.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MusteriFaturaProje.Services.Concrete
{
    public class SatisFaturalariRepository<T> : GenericRepository<T>, ISatisFaturalariRepository<T>  where T : class
    {
        protected readonly DatabaseContext _context;
        public SatisFaturalariRepository(DatabaseContext context) : base(context)
        {
            _context = context;

        }
        public List<SatisFaturasi> GetAll(bool satirlariDahilEt)
        {
            var satirlar = _context.SatisFaturasis.AsQueryable();
            if (satirlariDahilEt)
            {
                satirlar = satirlar.Include(x => x.SatisFaturasiSatirlaris);
            }
            return satirlar.ToList();
        }
    }
}
