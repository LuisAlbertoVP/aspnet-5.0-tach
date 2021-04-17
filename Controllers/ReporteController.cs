using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Policy;
using Microsoft.AspNetCore.Mvc;

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
            var query = "new(Id,Descripcion,Repuestos.Sum(Stock) as Stock,Repuestos.Sum(VentaDetalle.Sum(Cantidad)) as CantidadVentas,"
                + "Repuestos.Sum(CompraDetalle.Sum(Cantidad)) as CantidadCompras)";
            var categorias = await _context.Categorias.Where("Estado == true && EstadoTabla == true").Select(query)
                .ToDynamicArrayAsync();
            var marcas = await _context.Marcas.Where("Estado == true && EstadoTabla == true").Select(query)
                .ToDynamicArrayAsync();
            var ventas = await _context.Ventas.Where("Estado == true")
                .Select("new(VentaDetalle.Sum(Cantidad) as Cantidad,Fecha)")
                .ToDynamicArrayAsync();
            var compras = await _context.Compras.Where("Estado == true")
                .Select("new(CompraDetalle.Sum(Cantidad) as Cantidad,Fecha)")
                .ToDynamicArrayAsync();
            return Ok(new { categorias = categorias, marcas = marcas, ventas = ventas, compras = compras });
        }
    }
}