using Microsoft.EntityFrameworkCore;
using Tach.Models.Entities;

namespace Tach
{
    public class TachContext : DbContext
    {
        public TachContext (DbContextOptions<TachContext> options) : base(options) {}

        public DbSet<Usuario> Usuarios { get; set; }
        
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Repuesto> Repuestos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Modulo> Modulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Usuario>().Property(u => u.NombreUsuario).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Usuario>().Property(u => u.Nombres).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Usuario>().Property(u => u.Clave).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Cedula).IsRequired().HasMaxLength(10);
            modelBuilder.Entity<Usuario>().Property(u => u.Direccion).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Telefono).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Usuario>().Property(u => u.Celular).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Usuario>().Property(u => u.FechaNacimiento).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Usuario>().Property(u => u.Correo).IsRequired().HasMaxLength(320);
            modelBuilder.Entity<Usuario>().Property(u => u.FechaContratacion).IsRequired().HasColumnType("date");
            modelBuilder.Entity<Usuario>().Property(u => u.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Usuario>().Property(u => u.UsuarioModificacion).HasMaxLength(10);

            modelBuilder.Entity<Proveedor>().Property(p => p.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Proveedor>().Property(p => p.Telefono).HasMaxLength(25);
            modelBuilder.Entity<Proveedor>().Property(p => p.TipoProveedor).HasMaxLength(100);
            modelBuilder.Entity<Proveedor>().Property(p => p.Contacto).HasMaxLength(50);
            modelBuilder.Entity<Proveedor>().Property(p => p.TelefonoContacto).HasMaxLength(25);
            modelBuilder.Entity<Proveedor>().Property(p => p.CorreoContacto).HasMaxLength(320);
            modelBuilder.Entity<Proveedor>().Property(p => p.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Proveedor>().Property(p => p.UsuarioModificacion).HasMaxLength(10);

            modelBuilder.Entity<Repuesto>().Property(r => r.Codigo).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.Modelo).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.Epoca).HasMaxLength(50);
            modelBuilder.Entity<Repuesto>().Property(r => r.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Repuesto>().Property(r => r.UsuarioModificacion).HasMaxLength(10);

            modelBuilder.Entity<Categoria>().Property(c => c.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Categoria>().Property(c => c.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Categoria>().Property(c => c.UsuarioModificacion).HasMaxLength(10);
            modelBuilder.Entity<Categoria>().Ignore(c => c.Stock);

            modelBuilder.Entity<Marca>().Property(m => m.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Marca>().Property(m => m.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Marca>().Property(m => m.UsuarioModificacion).HasMaxLength(10);
            modelBuilder.Entity<Marca>().Ignore(m => m.Stock);

            modelBuilder.Entity<Rol>().Property(r => r.Descripcion).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Rol>().Property(r => r.UsuarioIngreso).HasMaxLength(10);
            modelBuilder.Entity<Rol>().Property(r => r.UsuarioModificacion).HasMaxLength(10);
        }
    }
}