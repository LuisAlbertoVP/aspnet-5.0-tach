using Microsoft.AspNetCore.Authorization;

namespace Tach.Models.Policy {
    internal class HasPermissionRequirement : IAuthorizationRequirement {
        public HasPermissionRequirement(string permission) { Permission = permission; }

        public string Permission { get; set;}
    }
}