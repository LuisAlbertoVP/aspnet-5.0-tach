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
    [Route("api/clientes")]
    [HasPermission("Clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly TachContext _context;

        public ClienteController(TachContext context) => _context = context;

        [HttpGet("{id}/ventas")]
        public async Task<IActionResult> GetVentas(string id) {
            var ventas = await _context.Ventas.Where("Estado == true").Where("Cliente.Id == @0", id).OrderBy("Fecha")
                .Select(QueryBuilder.VentasCliente.CamposConsulta).ToDynamicArrayAsync();
            return Ok(ventas);
        }


        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Cliente>(_context.Clientes.AsQueryable(), QueryBuilder.Clientes));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Cliente cliente) {
            if(new ClienteValidator().Validate(cliente).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddCliente({0})", JSON.Parse<Cliente>(cliente));
                    transaction.Commit();
                    return Ok(new Respuesta { Result = "Cliente actualizado correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Cliente no actualizado");
                }
            } else {
                return BadRequest("Algunos campos no son válidos");
            }
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Cliente cliente) {
            var newCliente = await _context.Clientes.FindAsync(id);
            if(newCliente != null) {
                newCliente.Estado = cliente.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Respuesta { Result = cliente.Estado ? "Cliente restaurado" : "Cliente reclidado" }) : 
                    StatusCode(304);
            }
            return NotFound("El cliente no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Cliente cliente) {
            var newCliente = await _context.Clientes.FindAsync(id);
            if(newCliente != null) {
                newCliente.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Respuesta { Result = "Cliente eliminado correctamente" }) : StatusCode(304);
            }
            return NotFound("El cliente no existe");
        }
    }
}