namespace Tach.Models.Helpers {
    public struct Field {

        public static string Base { 
            get { 
                return "new(Id,Descripcion,Repuestos.Count as Stock,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
            }
        }

        public static string Compras {
            get {
                return "new(Id,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,CompraDetalle.Select(new(Cantidad,new("
                    + "new(Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca," 
                    + "Repuesto.Codigo,Repuesto.Modelo,Repuesto.Epoca) as Repuesto)) as CompraDetalle)";
            }
        }

        public static string Proveedores { 
            get { 
                return "new(Id,Descripcion,Convenio,Telefono,Direccion,TipoProveedor,Contacto,TelefonoContacto,CorreoContacto,Estado,"
                    + "UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
            }
        }

        public static string Repuestos { 
            get { 
                return "new(Id,Codigo,new(Categoria.Id,Categoria.Descripcion) as Categoria,new(Marca.Id,Marca.Descripcion) as Marca,Modelo," 
                    + "Epoca,SubMarca,Stock,Precio,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
            }
        }

        public static string Roles { 
            get { 
                return "new(Id,Descripcion,Modulos,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
            }
        }

        public static string Usuarios { 
            get { 
                return "new(Id,NombreUsuario,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,Roles.Select(new(Id, Descripcion)) "
                    + "as Roles,FechaContratacion,Salario,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
            }
        }

        public static string Ventas {
            get {
                return "new(Id,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,VentaDetalle.Select(new(Cantidad,new("
                    + "new(Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca," 
                    + "Repuesto.Codigo,Repuesto.Modelo,Repuesto.Epoca) as Repuesto)) as VentaDetalle)";
            }
        }
    }
}