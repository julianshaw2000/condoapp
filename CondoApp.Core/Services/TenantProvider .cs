using CondoApp.Core.Enums;
using CondoApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;

namespace CondoApp.Core.Services
{
    public sealed class TenantProvider : ITenantProvider
    {
        private const string TenantIdHeaderName = "X-TenantId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetTenantId()
        {
            var context = _httpContextAccessor.HttpContext;

            // 🟢 If SuperAdmin, no TenantId required
            if (IsSuperAdmin())
            {
                // Return 0 or any value you use to indicate "all tenants"
                return 0;
            }

            // 🟡 Otherwise, require X-TenantId header
            if (context?.Request.Headers.TryGetValue(TenantIdHeaderName, out var tenantIdHeader) == true &&
                int.TryParse(tenantIdHeader, out int tenantId))
            {
                return tenantId;
            }

            // 🔴 If no tenantId found and not SuperAdmin, throw an error
            throw new ApplicationException("Tenant ID is not present in the request headers.");
        }

        public bool IsSuperAdmin()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.IsInRole(UserRole.SUPERADMIN.ToString()) ?? false;
        }
    }
}
