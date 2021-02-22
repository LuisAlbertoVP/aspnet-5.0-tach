using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Entities;
using Tach.Models.Helpers;
using Tach.Models.Policy;
using Tach.Models.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;

namespace Tach.Controllers
{
    [ApiController]
    [Route("api/compras")]
    [HasPermission("Compras")]
    public class CompraController : ControllerBase
    {
        private readonly TachContext _context;

        public CompraController(TachContext context) => _context = context;


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var query = "new(Id,Fecha,Cantidad,Total,Descripcion,Estado,UsuarioIngreso,FechaIngreso,UsuarioModificacion,FechaModificacion,"
                    + "CompraDetalle.Select(new(Cantidad,new(new(Repuesto.Categoria.Descripcion) as Categoria,new(Repuesto.Marca.Descripcion) " 
                    + "as Marca,Repuesto.Id,Repuesto.Codigo,Repuesto.Modelo,Repuesto.Epoca,Repuesto.Precio) as Repuesto)) as CompraDetalle)";
            var compra = await _context.Compras.Where("Estado == true").Where("Id == @0", id)
                .Select(query).FirstOrDefaultAsync();
            return Ok(new { compra = compra });
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Compra>(_context.Compras.AsQueryable(), QueryBuilder.Compras, false));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Compra compra) {
            if(new CompraValidator().Validate(compra).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL AddCompra({0})", JSON.Parse<Compra>(compra));
                    transaction.Commit();
                    return Ok(new Respuesta { Result = "Compra actualizada correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Compra no actualizada");
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
                return result > 0 ? Ok(new Respuesta { Result = compra.Estado ?  "Compra restaurada" : "Compra reciclada" }) : 
                    StatusCode(304);
            }
            return NotFound("La compra no existe");
        }
    }
}