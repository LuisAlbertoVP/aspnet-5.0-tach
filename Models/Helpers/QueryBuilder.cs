namespace Tach.Models.Helpers {
    public class QueryBuilder {

        public static Query Base { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Descripcion,Repuestos.Sum(Stock) as Stock,Repuestos.Sum(Stock * Precio) as Total," 
                        + "Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)",
                    SumaStock = "Repuestos.Sum(Stock)",
                    SumaTotal = "Repuestos.Sum(Stock * Precio)"
                };
            }
        }

        public static Query Compras {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,"
                        + "FechaModificacion)",
                    SumaStock = "Cantidad",
                    SumaTotal = "Total",
                };
            }
        }

        public static Query Clientes { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,TipoCliente,Estado,"
                        + "UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)",
                };
            }
        }

        public static Query Proveedores { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Descripcion,Convenio,Telefono,Direccion,TipoProveedor,Contacto,TelefonoContacto,"
                        + "CorreoContacto,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"
                }; 
            }
        }

        public static Query Repuestos { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Codigo,new(Categoria.Id,Categoria.Descripcion) as Categoria,new(Marca.Id,"
                        + "Marca.Descripcion) as Marca,Modelo,Epoca,SubMarca,Stock,Precio,Descripcion,Estado,UsuarioIngreso,"
                        + "FechaIngreso,UsuarioModificacion,FechaModificacion)",
                    SumaStock = "Stock",
                    SumaPrecio = "Precio",
                    SumaTotal = "Stock * Precio"
                };
            }
        }

        public static Query Roles { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Descripcion,Modulos,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"
                };
            }
        }

        public static Query Usuarios { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,NombreUsuario,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,Roles.Select("
                        + "new(Id, Descripcion)) as Roles,FechaContratacion,Salario,Estado,UsuarioIngreso,FechaIngreso,"
                        + "UsuarioModificacion,FechaModificacion)"
                };
            }
        }

        public static Query Ventas {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,Cantidad,Total,Descripcion,Direccion,new(Cliente.Nombres) as Cliente,Estado,"
                        + "UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)",
                    SumaStock = "Cantidad",
                    SumaTotal = "Total"
                };
            }
        }
    }
}