using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApi.Infrastructure;
using CondoApp.Core.Entities;
using CondoApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CondoApp.Data.Repositories;

public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context) { }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    // Expose generic CRUD methods for use via IUserRepository
    public new async Task<IEnumerable<ApplicationUser>> GetAllAsync() => await base.GetAllAsync();
    public new async Task<ApplicationUser?> GetByIdAsync(object id) => await base.GetByIdAsync(id);
    public new async Task AddAsync(ApplicationUser entity) => await base.AddAsync(entity);
    public new void Update(ApplicationUser entity) => base.Update(entity);
    public new void Delete(ApplicationUser entity) => base.Delete(entity);
}