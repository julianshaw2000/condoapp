using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Data;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;

namespace CondoApp.Data.Repositories;

public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(AppDbContext context) : base(context) { }

}
