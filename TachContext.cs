using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tach.Models.Entities;
using System;

namespace Tach
{
    public class TachContext : DbContext
    {
        public TachContext (DbContextOptions<TachContext> options) : base(options) {}

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Modulo> Modulos { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Repuesto> Repuestos { get; set; }

        public DbSet<Rol> Roles { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<CompraDetalle>()
                .HasKey(c => new { c.CompraId, c.RepuestoId });

            modelBuilder.Entity<CompraDetalle>().HasOne(ct => ct.Compra).WithMany(c => c.CompraDetalle)
                .HasForeignKey(ct => ct.CompraId);

            modelBuilder.Entity<CompraDetalle>().HasOne(ct => ct.Repuesto).WithMany(r => r.CompraDetalle)
                .HasForeignKey(ct => ct.RepuestoId);

            modelBuilder.Entity<VentaDetalle>()
                .HasKey(v => new { v.RepuestoId, v.VentaId });

            modelBuilder.Entity<VentaDetalle>().HasOne(vt => vt.Repuesto).WithMany(r => r.VentaDetalle)
                .HasForeignKey(vt => vt.RepuestoId);

            modelBuilder.Entity<VentaDetalle>().HasOne(vt => vt.Venta).WithMany(v => v.VentaDetalle)
                .HasForeignKey(vt => vt.VentaId);

            modelBuilder.Entity<Categoria>().Property(c => c.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Categoria>().Ignore(c => c.Stock);
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Categoria>());

            modelBuilder.Entity<Cliente>().Property(c => c.TipoCliente).IsRequired().HasMaxLength(25);
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration<Cliente>());

            modelBuilder.Entity<Compra>().Property(c => c.UsuarioIngreso).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Compra>().Property(c => c.FechaIngreso).IsRequired();

            modelBuilder.Entity<Marca>().Property(m => m.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Marca>().Ignore(m => m.Stock);
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Marca>());
            
            modelBuilder.Entity<Proveedor>().Property(p => p.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Proveedor>().Property(p => p.Telefono).HasMaxLength(25);
            modelBuilder.Entity<Proveedor>().Property(p => p.TipoProveedor).HasMaxLength(100);
            modelBuilder.Entity<Proveedor>().Property(p => p.Contacto).HasMaxLength(50);
            modelBuilder.Entity<Proveedor>().Property(p => p.TelefonoContacto).HasMaxLength(25);
            modelBuilder.Entity<Proveedor>().Property(p => p.CorreoContacto).HasMaxLength(320);
            modelBuilder.Entity<Proveedor>().Property(p => p.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Proveedor>().Property(p => p.UsuarioModificacion).HasMaxLength(10);
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Proveedor>());

            modelBuilder.Entity<Repuesto>().Property(r => r.Codigo).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.Modelo).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.Epoca).HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.SubMarca).HasMaxLength(50);
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Repuesto>());

            modelBuilder.Entity<Rol>().Property(r => r.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.ApplyConfiguration(new EntityTypeConfiguration<Rol>());

            modelBuilder.Entity<Usuario>().Property(u => u.NombreUsuario).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Usuario>().Property(u => u.Clave).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.FechaContratacion).IsRequired().HasColumnType("date");
            modelBuilder.ApplyConfiguration(new PersonTypeConfiguration<Usuario>());


            modelBuilder.Entity<Venta>().Property(v => v.UsuarioIngreso).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Venta>().Property(v => v.FechaIngreso).IsRequired();
        }
    }

    public class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class {
        public virtual void Configure(EntityTypeBuilder<T> builder) {
            builder.Property<string>("UsuarioIngreso").HasMaxLength(10);
            builder.Property<string>("UsuarioModificacion").HasMaxLength(10);
        }
    }

    public class PersonTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class {
        public override void Configure(EntityTypeBuilder<T> builder) {
            builder.Property<string>("Nombres").IsRequired().HasMaxLength(50);
            builder.Property<string>("Cedula").IsRequired().HasMaxLength(10);
            builder.Property<string>("Direccion").IsRequired();
            builder.Property<string>("Telefono").IsRequired().HasMaxLength(25);
            builder.Property<string>("Celular").IsRequired().HasMaxLength(25);
            builder.Property<DateTime>("FechaNacimiento").IsRequired().HasColumnType("date");
            builder.Property<string>("Correo").IsRequired().HasMaxLength(320);
            base.Configure(builder);
        }
    }
}