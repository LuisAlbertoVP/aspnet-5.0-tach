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
    [Route("api/categorias")]
    [HasPermission("Categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly TachContext _context;

        public CategoriaController(TachContext context) => _context = context;


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Categoria>(_context.Categorias.AsQueryable(), QueryBuilder.Base));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Categoria categoria) {
            if(new CategoriaValidator().Validate(categoria).IsValid) {
                var count = _context.Categorias.Where("Id == @0", categoria.Id).Count();
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddCategoria({0})", JSON.Parse<Categoria>(categoria));
                    transaction.Commit();
                    return Ok(new Mensaje { Texto = "Categoría " + (count == 0 ? "registrada" : "actualizada") + " correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest(count == 0 ? "Categoría no registrada" : "Categoría no actualizada");
                }
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Categoria categoria) {
            var newCategoria = await _context.Categorias.FindAsync(id);
            if(newCategoria != null) {
                newCategoria.Estado = categoria.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = categoria.Estado ? "Categoría restaurada" : "Categoría reciclada" }) : 
                    StatusCode(304);
            }
            return NotFound("La categoría no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Categoria categoria) {
            var newCategoria = await _context.Categorias.FindAsync(id);
            if(newCategoria != null) {
                newCategoria.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = "Categoría eliminada correctamente" }) : StatusCode(304);
            }
            return NotFound("La categoría no existe");
        }
    }
}