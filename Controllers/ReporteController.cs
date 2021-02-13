using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Entities;
using Tach.Models.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tach.Controllers
{
    [ApiController]
    [Route("api/reportes")]
    [HasPermission("Reportes")]
    public class ReporteController : ControllerBase
    {
        private readonly TachContext _context;

        public ReporteController(TachContext context) => _context = context;


        [HttpGet]
        public async Task<IActionResult> Get() {
            var categorias = await _context.Categorias.Where("Estado == true && EstadoTabla == true").OrderBy("Repuestos.Count desc")
                .Take(5).Select<Categoria>("new(Id, Descripcion, Repuestos.Count as Stock)").ToListAsync();
            var marcas = await _context.Marcas.Where("Estado == true && EstadoTabla == true").OrderBy("Repuestos.Count desc")
                .Take(5).Select<Marca>("new(Id, Descripcion, Repuestos.Count as Stock)").ToListAsync();
            var ventas = await _context.Ventas.Where("Estado == true").OrderBy("FechaIngreso")
                .Select("new(Cantidad,Total,FechaIngreso)").ToDynamicArrayAsync();
            var compras = await _context.Compras.Where("Estado == true").OrderBy("FechaIngreso")
                .Select("new(Cantidad,Total,FechaIngreso)").ToDynamicArrayAsync();
            return Ok(new { marcas = marcas, categorias = categorias, ventas = ventas, compras = compras });
        }
    }
}