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
    [Route("api/marcas")]
    [HasPermission("Marcas")]
    public class MarcaController : ControllerBase
    {
        private readonly TachContext _context;

        public MarcaController(TachContext context) => _context = context;


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Marca>(_context.Marcas.AsQueryable(), QueryBuilder.Base));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Marca marca) {
            if(new MarcaValidator().Validate(marca).IsValid) {
                var count = _context.Marcas.Where("Id == @0", marca.Id).Count();
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddMarca({0})", JSON.Parse<Marca>(marca));
                    transaction.Commit();
                    return Ok(new Mensaje { Texto = "Marca " + (count == 0 ? "registrada" : "actualizada") + " correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest(count == 0 ? "Marca no registrada" : "Marca no actualizada");
                }
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Marca marca) {
            var newMarca = await _context.Marcas.FindAsync(id);
            if(newMarca != null) {
                newMarca.Estado = marca.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = marca.Estado ? "Marca restaurada" : "Marca reciclada" }) : 
                    StatusCode(304);
            }
            return NotFound("La marca no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Marca marca) {
            var newMarca = await _context.Marcas.FindAsync(id);
            if(newMarca != null) {
                newMarca.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = "Marca eliminada correctamente" }) : StatusCode(304);
            }
            return NotFound("La marca no existe");
        }
    }
}