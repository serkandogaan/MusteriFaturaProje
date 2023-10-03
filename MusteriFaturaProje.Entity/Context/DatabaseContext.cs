using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MusteriFaturaProje.Entity.Models;

namespace MusteriFaturaProje.Entity.Context;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Malzeme> Malzemes { get; set; }

    public virtual DbSet<Musteri> Musteris { get; set; }

    public virtual DbSet<SatisFaturasi> SatisFaturasis { get; set; }

    public virtual DbSet<SatisFaturasiSatirlari> SatisFaturasiSatirlaris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MusteriFatura;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SatisFaturasi>(entity =>
        {
            entity.HasOne(d => d.Musteri).WithMany(p => p.SatisFaturasis).HasConstraintName("FK_SatisFaturasi_Musteri");
        });

        modelBuilder.Entity<SatisFaturasiSatirlari>(entity =>
        {
            entity.Property(e => e.Tutar).HasComputedColumnSql("([Miktar]*[BirimFiyat])", true);

            entity.HasOne(d => d.Malzeme).WithMany(p => p.SatisFaturasiSatirlaris).HasConstraintName("FK_SatisFaturasiSatirlari_Malzeme");

            entity.HasOne(d => d.SatisFaturasi).WithMany(p => p.SatisFaturasiSatirlaris)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SatisFaturasiSatirlari_SatisFaturasi");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
