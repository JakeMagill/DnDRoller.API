using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DnDRoller.API.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJWTToken(string username);
    }
}
