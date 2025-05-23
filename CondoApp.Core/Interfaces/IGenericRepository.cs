using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}