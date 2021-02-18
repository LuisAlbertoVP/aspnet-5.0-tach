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
    [Route("api/ventas")]
    [HasPermission("Ventas")]
    public class VentaController : ControllerBase
    {
        private readonly TachContext _context;

        public VentaController(TachContext context) => _context = context;


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Venta>(_context.Ventas.AsQueryable(), QueryBuilder.Ventas, false));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Venta venta) {
            if(new TransaccionValidator().Validate(venta).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddVenta({0})", JSON.Parse<Venta>(venta));
                    transaction.Commit();
                    return Ok(new Response { Result = "Venta agregada correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Venta no agregada");
                }
            } else {
                return BadRequest("Algunos campos no son válidos");
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Venta venta) {
            var newVenta = await _context.Ventas.FindAsync(id);
            if(newVenta != null) {
                newVenta.Estado = venta.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Response { Result = venta.Estado ?  "Venta restaurada" : "Venta reclidada" }) : 
                    StatusCode(304);
            }
            return NotFound("La venta no existe");
        }
    }
}