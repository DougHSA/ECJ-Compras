using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECJ_Compras.Services
{
    public class AuthService:IAuthService
    {
        private readonly EjcContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, EjcContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public Usuario Login(string username, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Login == username && u.Senha == password);
            if (usuario == null)
                throw new Exception("Usuario ou senha inválidos.");
            return usuario;
        }

        public JwtSecurityToken GerarToken(Usuario usuario)
        {
            // Authenticate user and generate claims
            var claims = new[]
            {
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.Nivel)
                        };
            var claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            return token;
        }
    }
}
