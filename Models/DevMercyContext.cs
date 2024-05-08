using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mercy_Dev.Models;

public partial class DevMercyContext : DbContext
{
    public DevMercyContext()
    {
    }

    public DevMercyContext(DbContextOptions<DevMercyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ClienteHasServicio> ClienteHasServicios { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("id_cliente");
            entity.Property(e => e.ApellidoCli)
                .HasMaxLength(45)
                .HasColumnName("Apellido_cli");
            entity.Property(e => e.CorreoCli)
                .HasMaxLength(45)
                .HasColumnName("Correo_cli");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.NombreCli)
                .HasMaxLength(45)
                .HasColumnName("Nombre_cli");
            entity.Property(e => e.TelefonoCli)
                .HasColumnType("int(11)")
                .HasColumnName("Telefono_cli");
        });

        modelBuilder.Entity<ClienteHasServicio>(entity =>
        {
            entity.HasKey(e => new { e.ClienteIdCliente, e.ServiciosIdServicios })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("cliente_has_servicios");

            entity.HasIndex(e => e.ClienteIdCliente, "fk_Cliente_has_servicios_Cliente1_idx");

            entity.HasIndex(e => e.ServiciosIdServicios, "fk_Cliente_has_servicios_servicios1_idx");

            entity.Property(e => e.ClienteIdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("Cliente_id_cliente");
            entity.Property(e => e.ServiciosIdServicios)
                .HasColumnType("int(11)")
                .HasColumnName("servicios_id_servicios");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");

            entity.HasOne(d => d.ClienteIdClienteNavigation).WithMany(p => p.ClienteHasServicios)
                .HasForeignKey(d => d.ClienteIdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cliente_has_servicios_Cliente1");

            entity.HasOne(d => d.ServiciosIdServiciosNavigation).WithMany(p => p.ClienteHasServicios)
                .HasForeignKey(d => d.ServiciosIdServicios)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Cliente_has_servicios_servicios1");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicios).HasName("PRIMARY");

            entity.ToTable("servicios");

            entity.Property(e => e.IdServicios)
                .HasColumnType("int(11)")
                .HasColumnName("id_servicios");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.InstalacionDrivers)
                .HasMaxLength(45)
                .HasColumnName("instalacion_drivers");
            entity.Property(e => e.InstalacionSO)
                .HasMaxLength(45)
                .HasColumnName("Instalacion_s.o");
            entity.Property(e => e.Limpieza).HasMaxLength(45);
            entity.Property(e => e.Mantenimiento).HasMaxLength(45);
            entity.Property(e => e.Valor)
                .HasColumnType("int(11)")
                .HasColumnName("valor");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
