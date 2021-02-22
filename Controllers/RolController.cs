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
    [Route("api/roles")]
    [HasPermission("Roles")]
    public class RolController : ControllerBase
    {
        private readonly TachContext _context;

        public RolController(TachContext context) => _context = context;


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Rol>(_context.Roles.AsQueryable(), QueryBuilder.Roles));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Rol rol) {
            if(new RolValidator().Validate(rol).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddRol({0})", JSON.Parse<Rol>(rol));
                    transaction.Commit();
                    return Ok(new Respuesta { Result = "Rol actualizado correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Rol no actualizado");
                }
            } else {
                return BadRequest("Algunos campos no son válidos");
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Rol rol) {
            var newRol = await _context.Roles.FindAsync(id);
            if(newRol != null) {
                newRol.Estado = rol.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Respuesta { Result = rol.Estado ? "Rol restaurado" : "Rol reclidado" }) : 
                    StatusCode(304);
            }
            return NotFound("El rol no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Rol rol) {
            var newRol = await _context.Roles.FindAsync(id);
            if(newRol != null) {
                newRol.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Respuesta { Result = "Rol eliminado correctamente" }) : StatusCode(304);
            }
            return NotFound("El rol no existe");
        }
    }
}