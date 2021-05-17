using System;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DynamicLinq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Tach.Models.Entities;
using Tach.Models.Helpers;
using Tach.Models.Validators;

namespace Tach.Controllers
{
    [ApiController]
    [Route("api")]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _cfg;
        private readonly TachContext _context;

        public IdentityController(IConfiguration cfg, TachContext context) {
            _cfg = cfg;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login(Usuario usuario) {
            if(new UsuarioNoAuthValidator().Validate(usuario).IsValid) {
                usuario = _context.Usuarios.Where("NombreUsuario == @0", usuario.NombreUsuario)
                    .Where("Clave == @0" , Crypto.HashPassword(usuario.Clave)).FirstOrDefault();
                if(usuario != null) {
                    return usuario.EstadoTabla && usuario.Estado ? CreateToken(usuario) : 
                        StatusCode(403, "Cuenta de usuario desactivada");
                }
                return Unauthorized("Las credenciales son incorrectas");
            }
            return BadRequest("Algunos campos no son válidos");
        }

        public IActionResult CreateToken(Usuario user) {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, (user.Id + user.NombreUsuario)),
                new Claim(JwtRegisteredClaimNames.Jti, System.Guid.NewGuid().ToString()),
                new Claim("Id", user.Id)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_cfg["Tokens:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var result = new JwtSecurityToken(_cfg["Tokens:Issuer"], _cfg["Tokens:Audience"], claims, 
                notBefore: DateTime.UtcNow ,expires: DateTime.UtcNow.AddHours(12), signingCredentials: credentials);
            var results = new {
                id = user.Id,
                nombres = user.Nombres,
                nombreUsuario = user.NombreUsuario,
                token = new {
                    id =  new JwtSecurityTokenHandler().WriteToken(result),
                    expiration = result.ValidTo
                }
            };
            return Ok(results);
        }

        [HttpPost("cuenta")]
        public IActionResult CrearCuenta(Usuario usuario) {
            if(new CuentaValidator().Validate(usuario).IsValid) {
                if(_context.Usuarios.Where("Id != @0 && NombreUsuario == @1", usuario.Id, usuario.NombreUsuario).Count() == 0) {
                    using var transaction = _context.Database.BeginTransaction();
                    try {
                        _context.Database.ExecuteSqlRaw("CALL AddAccount({0})", JSON.Parse<Usuario>(Crypto.HashPassword(usuario)));
                        transaction.Commit();
                        return Ok(new Mensaje { Texto = "La cuenta ha sido creada satisfactoriamente, solicite su activación" });
                    } catch (Exception) {
                        transaction.Rollback();
                        return BadRequest("La cuenta no ha sido creada");
                    }
                }
                return BadRequest("El nombre de usuario ya existe");
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpPost("cuenta/update")]
        [Authorize(AuthenticationSchemes="Autenticado")]
        public IActionResult ModificarCuenta(Usuario usuario) {
            if(new CuentaValidator().Validate(usuario).IsValid) {
                using var transaction = _context.Database.BeginTransaction();
                try {
                    _context.Database.ExecuteSqlRaw("CALL UpdateAccount({0})", JSON.Parse<Usuario>(Crypto.HashPassword(usuario)));
                    transaction.Commit();
                    return Ok(new Mensaje { Texto = "Cuenta actualizada correctamente" });
                } catch (Exception) {
                    transaction.Rollback();
                    return BadRequest("Cuenta no actualizada");
                }
            }
            return BadRequest("Algunos campos no son válidos");
        }

        [HttpGet("cuenta/{id}")]
        [Authorize(AuthenticationSchemes="Autenticado")]
        public async Task<IActionResult> GetForm(string id) {
            var usuario = await _context.Usuarios.Where("Estado == true").Where("EstadoTabla == true").Where("Id == @0", id)
                .Select<Usuario>("new(Id,NombreUsuario,Nombres,Cedula,Direccion,Telefono,Celular,FechaNacimiento,Correo)")
                .FirstOrDefaultAsync();
            return usuario != null ? Ok(usuario) : NotFound("No existe usuario");
        }

        [HttpGet("cuenta/{id}/roles")]
        [Authorize(AuthenticationSchemes="Autenticado")]
        public async Task<IActionResult> GetRolUsuario(string id) {
            var usuario = await _context.Usuarios.Where("Estado == true && EstadoTabla == true && Id == @0", id)
                .Select("new(Roles.Where(Estado == true && EstadoTabla == true).Select(new(Modulos)) as Roles)")
                .FirstOrDefaultAsync();
            return Ok(usuario);
        }
    }
}