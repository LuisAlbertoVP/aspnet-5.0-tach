﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tach;

namespace Tach.Migrations
{
    [DbContext(typeof(TachContext))]
    partial class TachContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("ModuloRol", b =>
                {
                    b.Property<int>("ModulosId")
                        .HasColumnType("int");

                    b.Property<string>("RolesId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ModulosId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ModuloRol");
                });

            modelBuilder.Entity("RolUsuario", b =>
                {
                    b.Property<string>("RolesId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuariosId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("RolesId", "UsuariosId");

                    b.HasIndex("UsuariosId");

                    b.ToTable("RolUsuario");
                });

            modelBuilder.Entity("Tach.Models.Entities.Categoria", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Tach.Models.Entities.Cliente", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320) CHARACTER SET utf8mb4");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("TipoCliente")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Tach.Models.Entities.Compra", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ProveedorId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Tach.Models.Entities.CompraDetalle", b =>
                {
                    b.Property<string>("CompraId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RepuestoId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("CompraId", "RepuestoId");

                    b.HasIndex("RepuestoId");

                    b.ToTable("CompraDetalle");
                });

            modelBuilder.Entity("Tach.Models.Entities.Marca", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("Tach.Models.Entities.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("Tach.Models.Entities.Proveedor", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Contacto")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("Convenio")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("CorreoContacto")
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Telefono")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("TelefonoContacto")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("TipoProveedor")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Tach.Models.Entities.Repuesto", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("CategoriaId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Epoca")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MarcaId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<double>("Precio")
                        .HasColumnType("double");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("SubMarca")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("MarcaId");

                    b.ToTable("Repuestos");
                });

            modelBuilder.Entity("Tach.Models.Entities.Rol", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Tach.Models.Entities.Usuario", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("Clave")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasMaxLength(320)
                        .HasColumnType("varchar(320) CHARACTER SET utf8mb4");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("EstadoTabla")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("NombreUsuario")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4");

                    b.Property<double>("Salario")
                        .HasColumnType("double");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("varchar(25) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Tach.Models.Entities.Venta", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ClienteId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("Estado")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("date");

                    b.Property<DateTime?>("FechaIngreso")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("FechaModificacion")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UsuarioIngreso")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.Property<string>("UsuarioModificacion")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Tach.Models.Entities.VentaDetalle", b =>
                {
                    b.Property<string>("RepuestoId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("VentaId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.HasKey("RepuestoId", "VentaId");

                    b.HasIndex("VentaId");

                    b.ToTable("VentaDetalle");
                });

            modelBuilder.Entity("ModuloRol", b =>
                {
                    b.HasOne("Tach.Models.Entities.Modulo", null)
                        .WithMany()
                        .HasForeignKey("ModulosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tach.Models.Entities.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RolUsuario", b =>
                {
                    b.HasOne("Tach.Models.Entities.Rol", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tach.Models.Entities.Usuario", null)
                        .WithMany()
                        .HasForeignKey("UsuariosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tach.Models.Entities.Compra", b =>
                {
                    b.HasOne("Tach.Models.Entities.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Tach.Models.Entities.CompraDetalle", b =>
                {
                    b.HasOne("Tach.Models.Entities.Compra", "Compra")
                        .WithMany("CompraDetalle")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tach.Models.Entities.Repuesto", "Repuesto")
                        .WithMany("CompraDetalle")
                        .HasForeignKey("RepuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compra");

                    b.Navigation("Repuesto");
                });

            modelBuilder.Entity("Tach.Models.Entities.Repuesto", b =>
                {
                    b.HasOne("Tach.Models.Entities.Categoria", "Categoria")
                        .WithMany("Repuestos")
                        .HasForeignKey("CategoriaId");

                    b.HasOne("Tach.Models.Entities.Marca", "Marca")
                        .WithMany("Repuestos")
                        .HasForeignKey("MarcaId");

                    b.Navigation("Categoria");

                    b.Navigation("Marca");
                });

            modelBuilder.Entity("Tach.Models.Entities.Venta", b =>
                {
                    b.HasOne("Tach.Models.Entities.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("Tach.Models.Entities.VentaDetalle", b =>
                {
                    b.HasOne("Tach.Models.Entities.Repuesto", "Repuesto")
                        .WithMany("VentaDetalle")
                        .HasForeignKey("RepuestoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tach.Models.Entities.Venta", "Venta")
                        .WithMany("VentaDetalle")
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Repuesto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Tach.Models.Entities.Categoria", b =>
                {
                    b.Navigation("Repuestos");
                });

            modelBuilder.Entity("Tach.Models.Entities.Compra", b =>
                {
                    b.Navigation("CompraDetalle");
                });

            modelBuilder.Entity("Tach.Models.Entities.Marca", b =>
                {
                    b.Navigation("Repuestos");
                });

            modelBuilder.Entity("Tach.Models.Entities.Repuesto", b =>
                {
                    b.Navigation("CompraDetalle");

                    b.Navigation("VentaDetalle");
                });

            modelBuilder.Entity("Tach.Models.Entities.Venta", b =>
                {
                    b.Navigation("VentaDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
