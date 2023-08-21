using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace ECJ_Compras.Controllers
{
    public class BaseController : Controller
    {
        public string VerificarUsuario()
        {
            string? userName = null;
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Decodifique o token JWT, acesse as informações necessárias (como o papel do usuário) e tome as decisões adequadas
                // Exemplo:
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(jwtToken);

                userName = token.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value; // Substitua "role" pelo nome real do claim que contém a role
            }
            return userName;
        }

        public string? VerificarRole()
        {
            string? userRole = null;
            var jwtToken = HttpContext.Request.Cookies["jwtToken"];
            if (!string.IsNullOrEmpty(jwtToken))
            {
                // Decodifique o token JWT, acesse as informações necessárias (como o papel do usuário) e tome as decisões adequadas
                // Exemplo:
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(jwtToken);

                userRole = token.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value; // Substitua "role" pelo nome real do claim que contém a role
            }
            return userRole;
        }
    }
}
