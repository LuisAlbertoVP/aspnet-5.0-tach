using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
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
            var cantidadPermisos = _context.Usuarios.Where("Id == @0", id).Where("Estado == true && EstadoTabla == true")
                .Where("Roles.Any(Estado == true && EstadoTabla == true && Modulos.Any(Descripcion.Contains(@0)))", requirement.Permission)
                .Count();
            if(cantidadPermisos > 0) {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            return Task.CompletedTask;
        }
    }
}