using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Data;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;

namespace CondoApp.Data.Repositories;


public class PersonRepository : GenericRepository<Person>, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context) { }

    public Task<Person> GetPersonByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    // public async Task<Person?> GetPersonByEmailAsync(string email)
    // {
    //     return await _context.Persons.FirstOrDefaultAsync(u => u.Email == email);
    // }
}
