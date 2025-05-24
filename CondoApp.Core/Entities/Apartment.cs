using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Entities
{

    public class Apartment

    {
        public int Id { get; set; }
        public string UnitNumber { get; set; } = string.Empty;
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }
    }

}