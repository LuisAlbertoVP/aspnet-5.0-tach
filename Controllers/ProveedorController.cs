using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Entities;
using Tach.Models.Helpers;
using Tach.Models.Policy;
using Tach.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tach.Controllers
{
    [ApiController]
    [Route("api/proveedores")]
    [HasPermission("Proveedores")]
    public class ProveedorController : ControllerBase
    {
        private readonly TachContext _context;

        public ProveedorController(TachContext context) => _context = context;

        [HttpGet("{id}/compras")]
        public async Task<IActionResult> GetCompras(string id) {
            var compras = await _context.Compras.Where("Estado == true").Where("Proveedor.Id == @0", id).OrderBy("Fecha")
                .Select(QueryBuilder.ComprasProveedor.CamposConsulta).ToDynamicArrayAsync();
            return Ok(compras);
        }


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Proveedor>(_context.Proveedores.AsQueryable(), QueryBuilder.Proveedores));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Proveedor proveedor) {
            if(new ProveedorValidator().Validate(proveedor).IsValid) {
                var count = _context.Proveedores.Where("Id == @0", proveedor.Id).Count();
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddProveedor({0})", JSON.Parse<Proveedor>(proveedor));
                    transaction.Commit();
                    return Ok(new Mensaje { Texto = "Proveedor " + (count == 0 ? "registrado" : "actualizado") + " correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest(count == 0 ? "Proveedor no registrado" : "Proveedor no actualizado");
                }
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Proveedor proveedor) {
            var newProveedor = await _context.Proveedores.FindAsync(id);
            if(newProveedor != null) {
                newProveedor.Estado = proveedor.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = proveedor.Estado ? "Proveedor restaurado" : "Proveedor reclidado" }) : 
                    StatusCode(304);
            }
            return NotFound("El proveedor no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Proveedor proveedor) {
            var newProveedor = await _context.Proveedores.FindAsync(id);
            if(newProveedor != null) {
                newProveedor.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = "Proveedor eliminado correctamente" }) : StatusCode(304);
            }
            return NotFound("El proveedor no existe");
        }
    }
}