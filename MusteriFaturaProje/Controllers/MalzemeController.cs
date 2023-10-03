using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusteriFaturaProje.Entity.Models;
using MusteriFaturaProje.Services.Abstract;

namespace MusteriFaturaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MalzemeController : ControllerBase
    {
        private readonly IGenericRepository<Malzeme> _malzeme;
        public MalzemeController(IGenericRepository<Malzeme> malzeme)
        {
            _malzeme = malzeme;

        }

        [HttpGet(Name = "TumMalzemeleriListele")]
        public List<Malzeme> TumMalzemeleriListele()
        {
            return _malzeme.GetAll();
        }

        [HttpPost(Name = "MalzemeEkle")]
        public ActionResult<Malzeme> MalzemeEkle(string malzemeAdi, string malzemeMarkaAdi, DateTime sonKullanmaTarihi, bool aktiflik)
        {
            if (ModelState.IsValid)
            {
                Malzeme yeniMalzeme = new Malzeme();
                yeniMalzeme.UrunAdi = malzemeAdi;
                yeniMalzeme.Markasi = malzemeMarkaAdi;
                yeniMalzeme.SonKullanmaTarihi = sonKullanmaTarihi;
                yeniMalzeme.Aktif = aktiflik;
                _malzeme.Add(yeniMalzeme);
                return Ok(yeniMalzeme);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete(Name = "MalzemeSil")]
        public ActionResult<Malzeme> MalzemeSil(int malzemeID)
        {
            var malzeme = _malzeme.GetByID(malzemeID);
            if (malzeme != null)
            {
                _malzeme.Delete(malzeme);
                return Ok(malzeme);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPut(Name = "MalzemeGuncelle")]
        public ActionResult<Malzeme> MalzemeGuncelle(int malzemeID, string yeniUrunAdi, string yeniMarkaAdi, DateTime yeniSonKullanmaTarihi, bool yeniAktiflik)
        {
            var malzeme = _malzeme.GetByID(malzemeID);
            if (malzeme != null && ModelState.IsValid)
            {
                malzeme.UrunAdi = yeniUrunAdi;
                malzeme.Markasi = yeniMarkaAdi;
                malzeme.SonKullanmaTarihi = yeniSonKullanmaTarihi;
                malzeme.Aktif = yeniAktiflik;
                _malzeme.Update(malzeme);
                return Ok(malzeme);
            }
            else
            {
                return NotFound();
            }
            
        }
    }
}
