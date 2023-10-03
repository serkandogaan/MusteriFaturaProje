using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusteriFaturaProje.Entity.Models;

[Table("Malzeme")]
public partial class Malzeme
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(150)]
    [Required(ErrorMessage = "Ürün adı zorunlu bir alandır.")]
    public string UrunAdi { get; set; } = null!;

    [StringLength(150)]
    [Required(ErrorMessage = "Ürün Markası zorunlu bir alandır.")]
    public string Markasi { get; set; } = null!;

    [Column(TypeName = "date")]
    [Required(ErrorMessage = "Son Kullanma Tarihi zorunlu bir alandır.")]
    public DateTime SonKullanmaTarihi { get; set; }

    [Required(ErrorMessage = "Ürün aktifliği zorunlu bir alandır.")]
    public bool Aktif { get; set; }

    [InverseProperty("Malzeme")]
    public virtual ICollection<SatisFaturasiSatirlari> SatisFaturasiSatirlaris { get; set; } = new List<SatisFaturasiSatirlari>();
}
