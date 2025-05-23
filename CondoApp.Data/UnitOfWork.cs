using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApi.Infrastructure;
using CondoApp.Core.Interfaces;
using CondoApp.Data.Repositories;

namespace CondoApp.Data;


public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository Users { get; }
    public IPersonRepository Persons { get; }
    public IApartmentRepository Apartments { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new UserRepository(_context);
        Persons = new PersonRepository(_context);
        Apartments = new ApartmentRepository(_context);
    }

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
}
