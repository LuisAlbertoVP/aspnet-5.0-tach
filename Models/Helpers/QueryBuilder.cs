namespace Tach.Models.Helpers {
    public class QueryBuilder {

        private static readonly Query query = new Query();

        public static Query Base { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Repuestos.Sum(Stock) as Stock,Repuestos.Sum(Precio) as Precio,Estado,UsuarioIngreso," 
                    + "FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = "Repuestos.Sum(Stock)";
                query.SumaPrecio = "Repuestos.Sum(Precio)";
                return query;
            }
        }

        public static Query Compras {
            get {
                query.CamposConsulta = "new(Id,Fecha,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = "Cantidad";
                query.SumaPrecio = "Total";
                return query;
            }
        }

        public static Query Proveedores { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Convenio,Telefono,Direccion,TipoProveedor,Contacto,TelefonoContacto,CorreoContacto,Estado,"
                    + "UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = null;
                query.SumaPrecio = null;
                return query; 
            }
        }

        public static Query Repuestos { 
            get { 
                query.CamposConsulta = "new(Id,Codigo,new(Categoria.Id,Categoria.Descripcion) as Categoria,new(Marca.Id,Marca.Descripcion) as Marca,"
                    + "Modelo,Epoca,SubMarca,Stock,Precio,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = "Stock";
                query.SumaPrecio = "Precio";
                return query;
            }
        }

        public static Query Roles { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Modulos,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = null;
                query.SumaPrecio = null;
                return query;
            }
        }

        public static Query Usuarios { 
            get { 
                query.CamposConsulta = "new(Id,NombreUsuario,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,Roles.Select("
                    + "new(Id, Descripcion)) as Roles,FechaContratacion,Salario,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
                query.SumaStock = null;
                query.SumaPrecio = null;
                return query;
            }
        }

        public static Query Ventas {
            get {
                query.CamposConsulta = "new(Id,Fecha,Cantidad,Total,Descripcion,Direccion,new(Cliente.Nombres) as Cliente,Estado,UsuarioIngreso,"
                    + "FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.SumaStock = "Cantidad";
                query.SumaPrecio = "Total";
                return query;
            }
        }
    }
}