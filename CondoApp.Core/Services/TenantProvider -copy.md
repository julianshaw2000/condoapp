using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using CondoApp.Core.Interfaces;

namespace CondoApp.Core.Services;

// ─── TenantProvider.cs ───

public class TenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _contextAccessor;

    public TenantProvider(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public string GetTenantId()
    {
        var httpContext = _contextAccessor.HttpContext;
        if (httpContext == null)
        {
            // Return a default tenant ID for non-HTTP contexts (like seeding, background tasks)
            return "1"; // Or return null
        }

        var user = httpContext.User;
        if (user == null || !user.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User is not authenticated.");
        }

        var tenantClaim = user.FindFirst("tenant_id");
        if (tenantClaim == null)
        {
            throw new UnauthorizedAccessException("Tenant ID claim is missing.");
        }

        return tenantClaim.Value;
    }
}


