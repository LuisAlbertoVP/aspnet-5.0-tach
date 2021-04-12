using System;
using System.IO;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Entities;
using Tach.Models.Helpers;
using Tach.Models.Policy;
using Tach.Models.Validators;
using Microsoft.AspNetCore.Http;
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


        [HttpPost("{id}/delete_file")]
        public async Task<IActionResult> DeleteFile(string id) {
            try {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", id);
                if(Directory.Exists(path)) {
                    Directory.Delete(path, true);
                    var compra = await _context.Compras.FindAsync(id);
                    compra.Ruta = null;
                    await _context.SaveChangesAsync(); 
                    return Ok(new Respuesta { Result = "Archivo eliminado correctamente" });
                }
                return NotFound("El archivo no existe");
            }
            catch(Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}/download_file")]
        public async Task<IActionResult> DownloadFile(string id) {
            try {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", id);
                if(Directory.Exists(path)) {
                    var filePath = Directory.GetFiles(path)[0];
                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open)) {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;
                    var mimeType = MimeTypes.GetMimeType()[Path.GetExtension(filePath).ToLower()];
                    return File(memory, mimeType, Path.GetFileName(filePath));
                }
                return NotFound();
            }
            catch(Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("{id}/upload_file")]
        public async Task<IActionResult> UploadFile(string id, IFormFile file) {
            try {
                if (file.Length > 0) {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "Resources", id);
                    if(Directory.Exists(path)) {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    var fullPath = Path.Combine(path, file.FileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        await file.CopyToAsync(stream);
                    }
                    return Ok();
                } else {
                    return BadRequest("El archivo no tiene contenido");
                }
            } catch(Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id) {
            var compra = await _context.Compras.Where("Estado == true").Where("Id == @0", id)
                .Select(QueryBuilder.Compra.CamposConsulta).FirstOrDefaultAsync();
            var proveedores = await _context.Proveedores.Where("Estado == true && EstadoTabla == true").OrderBy("Descripcion")
                .Select("new(Id,Descripcion)").ToDynamicArrayAsync();
            return Ok(new { compra = compra, proveedores = proveedores });
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
                    return Ok();
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