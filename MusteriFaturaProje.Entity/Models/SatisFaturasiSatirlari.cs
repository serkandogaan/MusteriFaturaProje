using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusteriFaturaProje.Entity.Models;

[Table("SatisFaturasiSatirlari")]
public partial class SatisFaturasiSatirlari
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Satır numarası zorunlu bir alandır.")]
    public int SatirNo { get; set; }

    [Required(ErrorMessage = "Miktar alanı zorunlu bir alandır.")]
    public byte Miktar { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    [Required(ErrorMessage = "Birim Fiyatı zorunlu bir alandır.")]
    public decimal BirimFiyat { get; set; }

    [Column("SatisFaturasiID")]
    [Required(ErrorMessage = "Satış Fatura ID'si zorunlu bir alandır.")]
    public int SatisFaturasiId { get; set; }

    [Column("MalzemeID")]
    [Required(ErrorMessage = "Malzeme ID'si zorunlu bir alandır.")]
    public int MalzemeId { get; set; }

    [Column(TypeName = "decimal(22, 2)")]
    public decimal? Tutar { get; set; }

    [ForeignKey("MalzemeId")]
    [InverseProperty("SatisFaturasiSatirlaris")]
    public virtual Malzeme Malzeme { get; set; } = null!;

    [ForeignKey("SatisFaturasiId")]
    [InverseProperty("SatisFaturasiSatirlaris")]
    public virtual SatisFaturasi SatisFaturasi { get; set; } = null!;
}
