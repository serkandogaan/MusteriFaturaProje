using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusteriFaturaProje.Entity.Models;
using MusteriFaturaProje.Services.Abstract;
using MusteriFaturaProje.Services.Concrete;

namespace MusteriFaturaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusteriController : ControllerBase
    {

        private readonly IGenericRepository<Musteri> _musteri;
        public MusteriController(IGenericRepository<Musteri> musteri)
        {
            _musteri = musteri;

        }

        [HttpGet(Name = "TumMusterileriListele")]
        public List<Musteri> TumMusterileriListele()
        {
            return _musteri.GetAll();
        }

        [HttpPost(Name = "MusteriEkle")]
        public ActionResult<Musteri> MusteriEkle(string musteriAdi, string musteriSoyadi, int TCVKNO, DateTime dogumTarihi, bool aktiflik)
        {
            if (ModelState.IsValid)
            {
                Musteri yeniMusteri = new Musteri();
                yeniMusteri.Ad = musteriAdi;
                yeniMusteri.Soyad = musteriSoyadi;
                yeniMusteri.Tcknvkn = TCVKNO;
                yeniMusteri.DogumTarihi = dogumTarihi;
                yeniMusteri.Aktif = aktiflik;
                _musteri.Add(yeniMusteri);
                return Ok(yeniMusteri);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete(Name = "MusteriSil")]
        public ActionResult<Musteri> MusteriSil(int musteriID) 
        {
            var musteri = _musteri.GetByID(musteriID);
            if (musteri != null) 
            {
                _musteri.Delete(musteri);
                return Ok(musteri);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPut(Name = "MusteriGuncelle")]
        public ActionResult<Musteri> MusteriGuncelle(int musteriID, string yeniMusteriAdi, string yeniMusteriSoyadi, int yeniTCVKNO, DateTime yeniDogumTarihi, bool yeniAktiflik)
        {
            var musteri = _musteri.GetByID(musteriID);
            if (musteri != null && ModelState.IsValid) 
            {
                musteri.Ad = yeniMusteriAdi;
                musteri.Soyad = yeniMusteriSoyadi;
                musteri.Tcknvkn = yeniTCVKNO;
                musteri.DogumTarihi = yeniDogumTarihi;
                musteri.Aktif = yeniAktiflik;
                _musteri.Update(musteri);
                return Ok(musteri);
            }
            else if (musteri == null)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
            
        }

    }

    
}
