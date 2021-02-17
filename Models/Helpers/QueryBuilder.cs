namespace Tach.Models.Helpers {
    public class QueryBuilder {

        private static readonly Query query = new Query();

        public static Query Base { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Repuestos.Count as Stock,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion," 
                    + "FechaModificacion)";
                query.CampoSumar = "Repuestos.Sum(Stock)";
                return query;
            }
        }

        public static Query Compras {
            get {
                query.CamposConsulta = "new(Id,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,CompraDetalle.Select("
                    + "new(Cantidad,new(new(Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca," 
                    + "Repuesto.Codigo,Repuesto.Modelo,Repuesto.Epoca) as Repuesto)) as CompraDetalle)";
                query.CampoSumar = "Cantidad";
                return query;
            }
        }

        public static Query Proveedores { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Convenio,Telefono,Direccion,TipoProveedor,Contacto,TelefonoContacto,"
                    +"CorreoContacto,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.CampoSumar = null;
                return query; 
            }
        }

        public static Query Repuestos { 
            get { 
                query.CamposConsulta = "new(Id,Codigo,new(Categoria.Id,Categoria.Descripcion) as Categoria,new(Marca.Id,Marca.Descripcion) as Marca,"
                    +"Modelo,Epoca,SubMarca,Stock,Precio,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.CampoSumar = "Stock";
                return query;
            }
        }

        public static Query Roles { 
            get { 
                query.CamposConsulta = "new(Id,Descripcion,Modulos,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)";
                query.CampoSumar = null; 
                return query;
            }
        }

        public static Query Usuarios { 
            get { 
                query.CamposConsulta = "new(Id,NombreUsuario,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,Roles.Select("
                    +"new(Id, Descripcion)) as Roles,FechaContratacion,Salario,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion)"; 
                query.CampoSumar = null;
                return query;
            }
        }

        public static Query Ventas {
            get {
                query.CamposConsulta = "new(Id,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,VentaDetalle.Select(new(Cantidad,new("
                    + "new(Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca," 
                    + "Repuesto.Codigo,Repuesto.Modelo,Repuesto.Epoca) as Repuesto)) as VentaDetalle)";
                query.CampoSumar = "Cantidad";
                return query;
            }
        }
    }
}