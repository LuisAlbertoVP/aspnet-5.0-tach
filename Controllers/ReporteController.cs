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
    [Route("api/reportes")]
    public class ReporteController : ControllerBase
    {
        private readonly TachContext _context;

        public ReporteController(TachContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            var categorias = await _context.Categorias.Where("Estado == true && EstadoTabla == true")
                .OrderBy("Descripcion").Select<Categoria>("new(Id, Descripcion, Repuestos.Count as Stock)").ToListAsync();
            var marcas = await _context.Marcas.Where("Estado == true && EstadoTabla == true")
                .OrderBy("Descripcion").Select<Marca>("new(Id, Descripcion, Repuestos.Count as Stock)").ToListAsync();
            return Ok(new { marcas = marcas, categorias = categorias });
        }
    }
}