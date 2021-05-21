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
    [Route("api/repuestos")]
    [HasPermission("Repuestos")]
    public class RepuestoController : ControllerBase
    {
        private readonly TachContext _context;

        public RepuestoController(TachContext context) => _context = context;


        [HttpGet("{id}/reporte")]
        public async Task<IActionResult> GetReporte(string id) {
            var repuesto = await _context.Repuestos.Where("Estado == true && EstadoTabla == true").Where("Id == @0", id)
                .Select(QueryBuilder.ReporteRepuesto.CamposConsulta)
                .FirstOrDefaultAsync();
            return Ok(repuesto);
        }

        [HttpGet("form")]
        public async Task<IActionResult> GetForm() {
            var categorias = await _context.Categorias.Where("Estado == true && EstadoTabla == true")
                .OrderBy("Descripcion").Select<Categoria>("new(Id, Descripcion)").ToListAsync();
            var marcas = await _context.Marcas.Where("Estado == true && EstadoTabla == true")
                .OrderBy("Descripcion").Select<Marca>("new(Id, Descripcion)").ToListAsync();
            return Ok(new { marcas = marcas, categorias = categorias });
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAll(Busqueda busqueda) {
            return Ok(await busqueda.BuildModel<Repuesto>(_context.Repuestos.AsQueryable(), QueryBuilder.Repuestos));
        }

        [HttpPost]
        public IActionResult InsertOrUpdate(Repuesto repuesto) {
            if(new RepuestoValidator().Validate(repuesto).IsValid) {
                if(_context.Repuestos.Where("Id != @0 && Codigo == @1 && EstadoTabla == true", repuesto.Id, repuesto.Codigo).Count() == 0) {
                    var count = _context.Repuestos.Where("Id == @0", repuesto.Id).Count();
                    using var transaction = _context.Database.BeginTransaction();
                    try {
                        _context.Database.ExecuteSqlRaw("CALL AddRepuesto({0})", JSON.Parse<Repuesto>(repuesto));
                        transaction.Commit();
                        return Ok(new Mensaje { Texto = "Repuesto " + (count == 0 ? "registrado" : "actualizado") + " correctamente" });
                    } catch (Exception) {
                        transaction.Rollback();
                        return BadRequest(count == 0 ? "Repuesto no registrado" : "Repuesto no actualizado");
                    }
                }
                return BadRequest("El código ya existe");
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("{id}/status")]
        public async Task<IActionResult> Status(string id, Repuesto repuesto) {
            var newRepuesto = await _context.Repuestos.FindAsync(id);
            if(newRepuesto != null) {
                newRepuesto.Estado = repuesto.Estado;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = repuesto.Estado ?  "Repuesto restaurado" : "Repuesto reclidado" }) : 
                    StatusCode(304);
            }
            return NotFound("El repuesto no existe");
        }

        [HttpPost("{id}/delete")]
        public async Task<IActionResult> Delete(string id, Repuesto repuesto) {
            var newRepuesto = await _context.Repuestos.FindAsync(id);
            if(newRepuesto != null) {
                newRepuesto.EstadoTabla = false;
                int result = await _context.SaveChangesAsync();
                return result > 0 ? Ok(new Mensaje { Texto = "Repuesto eliminado correctamente"}) : StatusCode(304);
            }
            return NotFound("El repuesto no existe");
        }
    }
}