using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prosperitopia.Application.Interface.Service.Authentication
{
    public interface IAuthService
    {
        bool ValidateToken(string token);
    }
}
