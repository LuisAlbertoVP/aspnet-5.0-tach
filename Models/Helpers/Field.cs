namespace Tach.Models.Helpers {
    public struct Field {
        
        public static string Usuarios { 
            get { 
                return "new(Id, NombreUsuario, Nombres, Cedula, Direccion, Telefono, Celular, FechaNacimiento, Correo, Roles,"
                    + "FechaContratacion, Salario, Estado, UsuarioIngreso, FechaIngreso, UsuarioModificacion, FechaModificacion)"; 
            }
        }

        public static string Roles { 
            get { 
                return "new(Id, Descripcion, Modulos, Estado, UsuarioIngreso, FechaIngreso, UsuarioModificacion, FechaModificacion)"; 
            }
        }

        public static string Repuestos { 
            get { 
                return "new(Id, Codigo, Marca, Categoria, Modelo, Epoca, SubMarca, Stock, Precio, Descripcion, Estado,"
                    + "UsuarioIngreso, FechaIngreso, UsuarioModificacion, FechaModificacion)"; 
            }
        }

        public static string Base { 
            get { 
                return "new(Id, Descripcion, Repuestos.Count as Stock, Estado, UsuarioIngreso, FechaIngreso, UsuarioModificacion,"
                    + "FechaModificacion)"; 
            }
        }

        public static string Proveedores { 
            get { 
                return "new(Id, Descripcion, Convenio, Telefono, Direccion, TipoProveedor, Contacto, TelefonoContacto,"
                    + "CorreoContacto, Estado, UsuarioIngreso, FechaIngreso, UsuarioModificacion, FechaModificacion)"; 
            }
        }
    }
}