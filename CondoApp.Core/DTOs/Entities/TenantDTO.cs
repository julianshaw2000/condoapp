using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.DTOs.Entities
{
    public class TenantDTO
    {
        //  public int Id { get; set; }
        public string TenantName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactEmail { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Add other Tenant fields except Id
    }
}

