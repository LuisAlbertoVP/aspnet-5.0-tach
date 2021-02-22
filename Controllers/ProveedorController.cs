using System;
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


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Proveedor>(_context.Proveedores.AsQueryable(), QueryBuilder.Proveedores));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Proveedor proveedor) {
            if(new ProveedorValidator().Validate(proveedor).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddProveedor({0})", JSON.Parse<Proveedor>(proveedor));
                    transaction.Commit();
                    return Ok(new Respuesta { Result = "Proveedor actualizado correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Proveedor no actualizado");
                }
            } else {
                return BadRequest("Algunos campos no son válidos");
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Proveedor proveedor) {
            var newProveedor = await _context.Proveedores.FindAsync(id);
            if(newProveedor != null) {
                newProveedor.Estado = proveedor.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Respuesta { Result = proveedor.Estado ? "Proveedor restaurado" : "Proveedor reclidado" }) : 
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
                return result > 0 ? Ok(new Respuesta { Result = "Proveedor eliminado correctamente" }) : StatusCode(304);
            }
            return NotFound("El proveedor no existe");
        }
    }
}