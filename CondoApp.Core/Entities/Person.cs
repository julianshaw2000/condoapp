using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.Enums;

namespace CondoApp.Core.Entities
{
    public class Person
    {

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public PersonType PersonType { get; set; }
        public int ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;

    }
}