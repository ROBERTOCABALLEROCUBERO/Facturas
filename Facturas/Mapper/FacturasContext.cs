using Facturas.Models;
using Microsoft.EntityFrameworkCore;

namespace Facturas.Mapper
{
    public class FacturasContext : DbContext
    {

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<LineaFactura> LineasFacturas { get; set; }
        public FacturasContext(DbContextOptions<FacturasContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
      
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes"); // Nombre de la tabla en la base de datos

                // Mapeo de las propiedades
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.NIF).HasMaxLength(20);
                entity.Property(e => e.Domicilio).HasMaxLength(200);
                entity.Property(e => e.Poblacion).HasMaxLength(100);
                entity.Property(e => e.CodigoPostal).HasMaxLength(10);
                entity.Property(e => e.Provincia).HasMaxLength(100);
                entity.Property(e => e.Pais).HasMaxLength(100);
                entity.Property(e => e.FechaAlta).HasColumnType("date");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Facturas").HasKey(e => e.NumeroFactura); // Nombre de la tabla en la base de datos

                // Mapeo de las propiedades
                
                entity.Property(e => e.NumeroFactura).ValueGeneratedNever();
                entity.Property(e => e.ClienteID).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.HasOne(e => e.Cliente)
                            .WithMany()
                            .HasForeignKey(e => e.ClienteID)
                            .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.LineasFactura)
                    .WithOne(l => l.Factura)
                    .HasForeignKey(l => l.FacturaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<LineaFactura>(entity =>
            {
                entity.ToTable("LineasFactura");
                entity.HasKey(e => e.LineaFacturaId);

                entity.Property(e => e.Concepto).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Unidades).IsRequired();
                entity.Property(e => e.Precio).IsRequired().HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Factura)
                          .WithMany()
                          .HasForeignKey(e => e.FacturaId)
                          .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }


    }

