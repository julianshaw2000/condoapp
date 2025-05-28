using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Interfaces
{
    // ─── ITenantProvider.cs ───
    public interface ITenantProvider
    {
        /// <summary>
        /// Gets the current TenantId from the JWT claims.
        /// </summary>
        /// <returns>The TenantId as a string (could be Guid or numeric ID).</returns>
        int GetTenantId();
    }

}