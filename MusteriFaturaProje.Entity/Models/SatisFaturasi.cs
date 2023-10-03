using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusteriFaturaProje.Entity.Models;

[Table("SatisFaturasi")]
public partial class SatisFaturasi
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Belge numarası zorunlu bir alandır.")]
    public long BelgeNo { get; set; }

    [Column(TypeName = "date")]
    [Required(ErrorMessage = "Belge tarihi zorunlu bir alandır.")]
    public DateTime BelgeTarihi { get; set; }

    [Column("MusteriID")]
    [Required(ErrorMessage = "Müşteri ID'si zorunlu bir alandır.")]
    public int MusteriId { get; set; }

    [ForeignKey("MusteriId")]
    [InverseProperty("SatisFaturasis")]
    public virtual Musteri Musteri { get; set; } = null!;

    [InverseProperty("SatisFaturasi")]
    public virtual ICollection<SatisFaturasiSatirlari> SatisFaturasiSatirlaris { get; set; } = new List<SatisFaturasiSatirlari>();
}
