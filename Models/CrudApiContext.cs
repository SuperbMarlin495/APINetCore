using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace APICoreWeb.Models;

public partial class CrudApiContext : DbContext
{
    public CrudApiContext()
    {
    }

    public CrudApiContext(DbContextOptions<CrudApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Informacion> Informacions { get; set; }

    public virtual DbSet<Trabajo> Trabajos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Informacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Informac__3213E83F6240EAC4");

            entity.ToTable("Informacion");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Trabajo>(entity =>
        {
            entity.HasKey(e => e.IdTrabajo).HasName("PK__Trabajo__4FB29E34673C9CE0");

            entity.ToTable("Trabajo");

            entity.Property(e => e.Empresa)
                .HasMaxLength(55)
                .IsUnicode(false);
            entity.Property(e => e.Puesto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Turno)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.oTrabajo).WithMany(p => p.Trabajos)
                .HasForeignKey(d => d.IdInformacion)
                .HasConstraintName("FK_IdInformacion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
