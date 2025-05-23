using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondoApp.Core.DTOs;

namespace CondoApp.Core.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserTokenPayload payload);
}