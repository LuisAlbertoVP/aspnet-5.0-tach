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
    [Route("api/usuarios")]
    [HasPermission("Usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly TachContext _context;

        public UsuarioController(TachContext context) => _context = context;


        [HttpGet("form")]
        public async Task<IActionResult> GetForm() {
            var roles = await _context.Roles.Where("Estado == true && EstadoTabla == true")
                .OrderBy("Descripcion").Select<Rol>("new(Id, Descripcion)").ToListAsync();
            return Ok(new { roles = roles });
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Usuario>(_context.Usuarios.AsQueryable(), QueryBuilder.Usuarios));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Usuario usuario) {
            if(new UsuarioValidator().Validate(usuario).IsValid) {
                if(_context.Usuarios.Where("Id != @0 && NombreUsuario == @1", usuario.Id, usuario.NombreUsuario).Count() == 0) {
                    var count = _context.Usuarios.Where("Id == @0", usuario.Id).Count();
                    using var transaction = _context.Database.BeginTransaction();
                    try {
                        _context.Database.ExecuteSqlRaw("CALL AddUsuario({0})", JSON.Parse<Usuario>(Crypto.HashPassword(usuario)));
                        transaction.Commit();
                        return Ok(new Mensaje { Texto = "Usuario " + (count == 0 ? "registrado" : "actualizado") + " correctamente" });
                    } catch (Exception) {
                        transaction.Rollback();
                        return BadRequest(count == 0 ? "Usuario no registrado" : "Usuario no actualizado");
                    }
                }
                return BadRequest("El nombre de usuario ya existe");
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Usuario usuario) {
            var newUsuario = await _context.Usuarios.FindAsync(id);
            if(newUsuario != null) {
                newUsuario.Estado = usuario.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = usuario.Estado ? "Usuario restaurado" : "Usuario reclidado" }) : 
                    StatusCode(304);
            }
            return NotFound("El usuario no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Usuario usuario) {
            var newUsuario = await _context.Usuarios.FindAsync(id);
            if(newUsuario != null) {
                newUsuario.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = "Usuario eliminado correctamente" }) : StatusCode(304);
            }
            return NotFound("El usuario no existe");
        }
    }
}