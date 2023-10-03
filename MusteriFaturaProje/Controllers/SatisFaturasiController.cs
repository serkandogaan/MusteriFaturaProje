using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusteriFaturaProje.Entity.Models;
using MusteriFaturaProje.Services.Abstract;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace MusteriFaturaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisFaturasiController : ControllerBase
    {

        private readonly IGenericRepository<SatisFaturasiSatirlari> _satisFaturalariSatirlari;
        private readonly IGenericRepository<Musteri> _musteri;
        private readonly IGenericRepository<Malzeme> _malzeme;
        private readonly ISatisFaturalariRepository<SatisFaturasi> _satisFaturalari;
        public SatisFaturasiController(ISatisFaturalariRepository<SatisFaturasi> satisFaturalari, IGenericRepository<SatisFaturasiSatirlari> satisFaturalariSatirlari, IGenericRepository<Musteri> musteri, IGenericRepository<Malzeme> malzeme)
        {
            _satisFaturalariSatirlari = satisFaturalariSatirlari;
            _musteri = musteri;
            _malzeme = malzeme;
            _satisFaturalari = satisFaturalari;
        }


        [HttpGet(Name = "SatisFaturalariniListele")]
        public object SatisFaturalariniListele(bool satirlarCekilsinMi)
        {
            if (satirlarCekilsinMi)
            {
                var satisFaturalari = _satisFaturalari.GetAll(true);
                return satisFaturalari;
            }
            else
            {
                var satisFaturalari = _satisFaturalari.GetAll(false);
                return satisFaturalari;
            }
        }


        // Buradaki Müşteri ID, yeni bir sipariş geldiğinde siparişi veren kullanıcının ID'si oluyor.

        // Satır No hangi alandan alınıyor bilemedim.

        // Malzeme ID' ise Malzeme tablosundaki ürün ID'leri.

        [HttpPost(Name = "SatisFaturasiEkle")]
        public ActionResult<SatisFaturasi> SatisFaturasiEkle(int belgeNo, DateTime belgeTarihi, int musteriID)
        {
            if ((_musteri.GetByID(musteriID).Aktif == true && ModelState.IsValid == true))
            {
                SatisFaturasi yeniSatisFaturasi = new SatisFaturasi();
                yeniSatisFaturasi.BelgeNo = belgeNo;
                yeniSatisFaturasi.BelgeTarihi = belgeTarihi;
                yeniSatisFaturasi.MusteriId = musteriID;
                _satisFaturalari.Add(yeniSatisFaturasi);
                return Ok(yeniSatisFaturasi);                            
            }
            else if (ModelState.IsValid == false)
            {
                return BadRequest();
            }
            else
            {
                return NotFound();
            }
            
        }
      
        // bir satış faturasının bir den fazla satış fatura satırı olabilir.
        // bu yüzden foreach dönerek satis fatura ID'sine eşit olan tüm satırları siliyorum.

        [HttpDelete(Name = "SatisFaturasiSil")]
        public ActionResult<SatisFaturasi> SatisFaturasiSil(int satisFaturaID)
        {
            var satisFaturasi = _satisFaturalari.GetByID(satisFaturaID);

            if (satisFaturasi != null)
            {
                foreach (var item in _satisFaturalariSatirlari.GetAll(x => x.SatisFaturasiId == satisFaturaID))
                {
                    _satisFaturalariSatirlari.Delete(item);
                }
                _satisFaturalari.Delete(satisFaturasi);
                return Ok(satisFaturasi);
            }
            else
            {
                return NotFound();
            }          
        }

        [HttpPut(Name = "SatisFaturasiGuncelle")]
        public ActionResult<SatisFaturasi> SatisFaturasiGuncelle(int satisFaturaID, int belgeNo, DateTime belgeTarigi, int musteriID)
        {
            var satisFaturasi = _satisFaturalari.GetByID(satisFaturaID);
            if (satisFaturasi != null && ModelState.IsValid)
            {
                satisFaturasi.BelgeNo = belgeNo;
                satisFaturasi.BelgeTarihi = belgeTarigi;
                satisFaturasi.MusteriId = musteriID;
                _satisFaturalari.Update(satisFaturasi);
                return Ok(satisFaturasi);
            }
            else if (satisFaturasi ==null)
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