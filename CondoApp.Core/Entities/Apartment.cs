using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Entities
{

    public class Apartment

    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        // public Tenant Tenant { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}