using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.DTOs.Entities
{
    public class ApartmentDTO
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string? OwnerId { get; set; }

    }
}