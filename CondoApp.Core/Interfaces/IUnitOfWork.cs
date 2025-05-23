using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IApartmentRepository Apartments { get; }
        IPersonRepository Persons { get; }
        Task<int> CompleteAsync(); // Save chang
    }


}