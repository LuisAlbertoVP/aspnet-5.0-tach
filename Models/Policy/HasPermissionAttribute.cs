using System;
using Microsoft.AspNetCore.Authorization;

namespace Tach.Models.Policy {
    [AttributeUsage(AttributeTargets.Class)]
    internal class HasPermissionAttribute : AuthorizeAttribute {
        const string POLICY_PREFIX = "HasPermission";

        public HasPermissionAttribute(string permission) => Permission = permission;

        public string Permission {
            get {
                return Policy.Substring(POLICY_PREFIX.Length);
            }
            set {
                Policy = $"{POLICY_PREFIX}{value}";
            }
        }
    }
}