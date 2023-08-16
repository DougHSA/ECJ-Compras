using Dominio.Models;
using System.IdentityModel.Tokens.Jwt;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IAuthService
    {
        Usuario Login(string username, string password);
        JwtSecurityToken GerarToken(Usuario usuario);
    }
}
