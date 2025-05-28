using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CondoApp.Core.Entities
{
    public class Person
    {
        public int Id { get; set; }

        public int TenantId { get; set; }
        // public Tenant Tenant { get; set; }
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        public PersonType PersonType { get; set; }
        // public int ApartmentId { get; set; }
        // public Apartment Apartment { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}