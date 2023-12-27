using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebPersonal_API.Modelos;

namespace WebPersonal_API.Datos;

public partial class PersonalDbContext : DbContext
{
    public PersonalDbContext()
    {
    }

    public PersonalDbContext(DbContextOptions<PersonalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CBarrio> CBarrios { get; set; }

    public virtual DbSet<CCatcar> CCatcars { get; set; }

    public virtual DbSet<CMunici> CMunicis { get; set; }

    public virtual DbSet<CProvin> CProvins { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-RBV1DEH;Database=Personal;TrustServerCertificate=true;Trusted_Connection=true;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CBarrio>(entity =>
        {
            entity.ToTable("c_barrio", tb => tb.HasComment("Barrios"));

            entity.Property(e => e.CodBarrio)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.CodMunici)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.CodProvin)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.NomBarrio).HasDefaultValue("");

            entity.HasOne(d => d.CodProvinNavigation).WithMany(p => p.CBarrios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_c_barrio_cod_provin");

            entity.HasOne(d => d.CMunici).WithMany(p => p.CBarrios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_c_barrio_munici");
        });

        modelBuilder.Entity<CCatcar>(entity =>
        {
            entity.ToTable("c_catcar", tb => tb.HasTrigger("utr_c_catcar"));

            entity.Property(e => e.CodCatcar)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.NomCatcar).HasDefaultValue("");
        });

        modelBuilder.Entity<CMunici>(entity =>
        {
            entity.ToTable("c_munici", tb => tb.HasTrigger("utr_c_munici"));

            entity.Property(e => e.CodProvin)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.CodMunici)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.NomMunici).HasDefaultValue("");

            entity.HasOne(d => d.CodProvinNavigation).WithMany(p => p.CMunicis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_c_munici_cod_provin");
        });

        modelBuilder.Entity<CProvin>(entity =>
        {
            entity.ToTable("c_provin", tb => tb.HasTrigger("utr_c_provin"));

            entity.Property(e => e.CodProvin)
                .HasDefaultValue("")
                .IsFixedLength();
            entity.Property(e => e.NomProvin).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
