using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Security.Interface
{
    public interface IJwtAuthenticationManager
    {
        string GenerateJwtToken();
    }
}
