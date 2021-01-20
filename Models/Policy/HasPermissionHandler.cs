using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Tach.Models.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;

namespace Tach.Models.Policy {
    internal class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement> {
        private readonly IServiceProvider _serviceProvider;

        public HasPermissionHandler(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement) {
            if (!context.User.HasClaim(c => c.Type == "Id")) {
                return Task.CompletedTask;
            }
            var id = context.User.FindFirst(c => c.Type == "Id").Value;
            using var scope = _serviceProvider.CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<TachContext>();
            var usuario = _context.Usuarios.Where("Estado == true").Where("EstadoTabla == true").Where("Id == @0", id)
                .Select(u => new Usuario { Roles = u.Roles.Select(r => new Rol { Modulos = r.Modulos }).ToList()})
                .FirstOrDefault();
            if(usuario != null) {
                foreach(var rol in usuario.Roles) {
                    foreach(var modulo in rol.Modulos) {
                        if (modulo.Descripcion == requirement.Permission) {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }            
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}