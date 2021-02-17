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
    [Route("api/compras")]
    [HasPermission("Compras")]
    public class CompraController : ControllerBase
    {
        private readonly TachContext _context;

        public CompraController(TachContext context) => _context = context;


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Compra>(_context.Compras.AsQueryable(), QueryBuilder.Compras, false));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Compra compra) {
            if(new TransaccionValidator().Validate(compra).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL InsertCompra({0})", JSON.Parse<Compra>(compra));
                    transaction.Commit();
                    return Ok(new Response { Result = "Compra agregada correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Compra no agregada");
                }
            } else {
                return BadRequest("Algunos campos no son válidos");
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Compra compra) {
            var newCompra = await _context.Compras.FindAsync(id);
            if(newCompra != null) {
                newCompra.Estado = compra.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Response { Result = compra.Estado ?  "Compra restaurada" : "Compra reciclada" }) : 
                    StatusCode(304);
            }
            return NotFound("La compra no existe");
        }
    }
}