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

        public static Query Compra {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion,"
                        + "new(Proveedor.Id) as Proveedor,Numero,Vendedor,SoldTo,ShipTo,Ruta,CompraDetalle.Select(new(Cantidad,new(new(" 
                        + "Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca,Repuesto.Id,Repuesto.Codigo,"
                        + "Repuesto.Modelo,Repuesto.Epoca,Repuesto.Precio) as Repuesto)) as CompraDetalle)"
                };
            }
        }

        public static Query Compras {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,Numero,Vendedor,SoldTo,ShipTo,Ruta,CompraDetalle.Sum(Cantidad) as Cantidad,CompraDetalle.Sum("
                        + "Cantidad * Repuesto.Precio) as Total,Descripcion,new(Proveedor.Descripcion) as Proveedor,Estado,UsuarioIngreso,"
                        + "FechaIngreso,UsuarioModificacion,FechaModificacion)",
                    SumaStock = "CompraDetalle.Sum(Cantidad)",
                    SumaTotal = "CompraDetalle.Sum(Cantidad * Repuesto.Precio)",
                };
            }
        }

        public static Query Clientes { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo,TipoCliente,Estado,"
                        + "Ventas.Sum(VentaDetalle.Sum(Cantidad)) as TotalVentas,UsuarioIngreso,FechaIngreso,"
                        + "UsuarioModificacion,FechaModificacion)",
                };
            }
        }

        public static Query Proveedores { 
            get {
                return new Query {
                    CamposConsulta = "new(Id,Descripcion,Telefono,Direccion,Correo,WebSite,Estado,UsuarioIngreso,FechaIngreso,"
                        + "UsuarioModificacion,FechaModificacion)"
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

        public static Query Venta {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,Descripcion,Direccion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,"
                    + "FechaModificacion,new(Cliente.Id) as Cliente,VentaDetalle.Select(new(Cantidad,new(new(Repuesto.Categoria." 
                    + "Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) as Marca,Repuesto.Id,Repuesto.Codigo,Repuesto.Modelo,"
                    + "Repuesto.Epoca,Repuesto.Precio) as Repuesto)) as VentaDetalle)"
                };
            }
        }

        public static Query Ventas {
            get {
                return new Query {
                    CamposConsulta = "new(Id,Fecha,VentaDetalle.Sum(Cantidad) as Cantidad,VentaDetalle.Sum(Cantidad * Repuesto.Precio) "
                        + "as Total,Descripcion,Direccion,new(Cliente.Nombres) as Cliente,Estado,UsuarioIngreso,FechaIngreso,"
                        + "UsuarioModificacion,FechaModificacion)",
                    SumaStock = "VentaDetalle.Sum(Cantidad)",
                    SumaTotal = "VentaDetalle.Sum(Cantidad * Repuesto.Precio)"
                };
            }
        }
    }
}