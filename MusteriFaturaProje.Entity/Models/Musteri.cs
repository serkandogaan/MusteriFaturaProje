using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MusteriFaturaProje.Entity.Models;

[Table("Musteri")]
public partial class Musteri
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(150)]
    [Required(ErrorMessage = "Müşteri adı zorunlu bir alandır.")]
    public string Ad { get; set; } = null!;

    [StringLength(150)]
    [Required(ErrorMessage = "Müşteri soyadı zorunlu bir alandır.")]
    public string Soyad { get; set; } = null!;

    [Column("TCKNVKN")]
    [Required(ErrorMessage = "Müşteri TC Kimlik / Vatandaşlık Numarası zorunlu bir alandır.")]
    public long Tcknvkn { get; set; }

    [Column(TypeName = "date")]
    [Required(ErrorMessage = "Müşteri doğum tarihi zorunlu bir alandır.")]
    public DateTime DogumTarihi { get; set; }

    [Required(ErrorMessage = "Müşteri aktifliği zorunlu bir alandır.")]
    public bool Aktif { get; set; }

    [InverseProperty("Musteri")]
    public virtual ICollection<SatisFaturasi> SatisFaturasis { get; set; } = new List<SatisFaturasi>();
}
