using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Tach.Models.Policy {
    internal class HasPermissionProvider : IAuthorizationPolicyProvider {
        const string POLICY_PREFIX = "HasPermission";

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName) {
            if (policyName.StartsWith(POLICY_PREFIX, StringComparison.OrdinalIgnoreCase)) {
                var permission = policyName.Substring(POLICY_PREFIX.Length);
                var policy = new AuthorizationPolicyBuilder("Autenticado");
                policy.AddRequirements(new HasPermissionRequirement(permission));
                return Task.FromResult(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() {
            var policy = new AuthorizationPolicyBuilder("Autenticado").RequireAuthenticatedUser();
            return Task.FromResult(policy.Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);
    }
}