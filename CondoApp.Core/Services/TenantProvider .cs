using CondoApp.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;


namespace CondoApp.Core.Services


{
    public sealed class TenantProvider


    : ITenantProvider
    {
        private const string TenantIdHeaderName = "X-TenantId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetTenantId()
        {
            var tenantIdHeader = _httpContextAccessor.HttpContext?
                .Request
                .Headers[TenantIdHeaderName];

            if (!tenantIdHeader.HasValue ||
                !int.TryParse(tenantIdHeader.Value, out int tenantId)
                )
            {
                // throw new ApplicationException("Tenant ID is not present");
                tenantId = 2;
            }

            return tenantId;
        }
    }
}
