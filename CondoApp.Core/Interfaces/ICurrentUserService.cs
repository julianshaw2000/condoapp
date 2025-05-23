using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CondoApp.Core.Interfaces
{
    public interface ICurrentUserService
    {

        string UserId { get; }
        string Role { get; }
        string? CondoId { get; }

    }
}