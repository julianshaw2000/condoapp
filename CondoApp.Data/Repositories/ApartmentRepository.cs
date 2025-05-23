using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApi.Infrastructure;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;

namespace CondoApp.Data.Repositories;

public class ApartmentRepository : GenericRepository<Apartment>, IApartmentRepository
{
    public ApartmentRepository(AppDbContext context) : base(context) { }

    // public   Task<Apartment?> GetApartmentByNumberAsync(string number)
    // {
    //     // return await _context.Apartments.FirstOrDefaultAsync(a => a.Number == number);
    // }
}
