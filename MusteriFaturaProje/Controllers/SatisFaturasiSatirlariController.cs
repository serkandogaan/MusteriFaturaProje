using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusteriFaturaProje.Entity.Models;
using MusteriFaturaProje.Services.Abstract;

namespace MusteriFaturaProje.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SatisFaturasiSatirlariController : ControllerBase
    {

        private readonly IGenericRepository<SatisFaturasiSatirlari> _satisFaturasiSatirlari;
        private readonly IGenericRepository<Malzeme> _malzeme;
        public SatisFaturasiSatirlariController(IGenericRepository<SatisFaturasiSatirlari> satisFaturasiSatirlari, IGenericRepository<Malzeme> malzeme)
        {
            _satisFaturasiSatirlari = satisFaturasiSatirlari;
            _malzeme = malzeme;
        }

        [HttpGet("{satisFaturaID:int}", Name = "SatisFaturasiSatiriListele")]
        public ActionResult<List<SatisFaturasiSatirlari>> SatisFaturasiSatiriListele(int satisFaturaID)
        {
            var satisFaturasiSatiri = _satisFaturasiSatirlari.GetAll(x => x.SatisFaturasiId == satisFaturaID);
            if (satisFaturasiSatiri != null)
            {
                return satisFaturasiSatiri;
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPost(Name = "SatisFaturasiSatiriEkle")]
        public ActionResult<SatisFaturasiSatirlari> SatisFaturasiSatiriEkle(int satirNo, byte miktar, decimal birimFiyat, int satisFaturaID, int malzemeID)
        {
            if (_malzeme.GetByID(malzemeID).Aktif == true && ModelState.IsValid)
            {
                SatisFaturasiSatirlari satisFaturasiSatirlari = new SatisFaturasiSatirlari();
                satisFaturasiSatirlari.SatirNo = satirNo;
                satisFaturasiSatirlari.Miktar = miktar;
                satisFaturasiSatirlari.BirimFiyat = birimFiyat;
                satisFaturasiSatirlari.MalzemeId = malzemeID;
                satisFaturasiSatirlari.SatisFaturasiId = satisFaturaID;
                _satisFaturasiSatirlari.Add(satisFaturasiSatirlari);
                return Ok(satisFaturasiSatirlari);
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpDelete(Name = "SatisFaturasiSatiriSil")]
        public ActionResult<SatisFaturasiSatirlari> SatisFaturasiSatiriSil(int satisFaturasiSatirID)
        {
            
            var satisFaturasiSatiri = _satisFaturasiSatirlari.GetByID(satisFaturasiSatirID);
            if (satisFaturasiSatiri != null)
            {
                _satisFaturasiSatirlari.Delete(satisFaturasiSatiri);
                return Ok(satisFaturasiSatiri);
            }
            else
            {
                return NotFound();
            }
            
        }

        [HttpPut(Name = "SatisFaturaSatiriGuncelle")]
        public ActionResult<SatisFaturasiSatirlari> SatisFaturaSatiriGuncelle(int satisFaturaSatirID, int satirNo, byte miktar, decimal birimFiyat, int satisFaturaID, int malzemeID)
        {
            var satisFaturaSatiri = _satisFaturasiSatirlari.GetByID(satisFaturaSatirID);

            if (satisFaturaSatiri != null)
            {
                satisFaturaSatiri.SatirNo = satirNo;
                satisFaturaSatiri.Miktar = miktar;
                satisFaturaSatiri.BirimFiyat = birimFiyat;
                satisFaturaSatiri.SatisFaturasiId = satisFaturaID;
                satisFaturaSatiri.MalzemeId = malzemeID;
                _satisFaturasiSatirlari.Update(satisFaturaSatiri);
                return Ok(satisFaturaSatiri);
            }
            else
            {
                return NotFound();
            }

            
        }

    }
}
