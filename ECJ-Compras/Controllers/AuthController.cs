﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ECJ_Compras.Dto;
using Dominio.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using ECJ_Compras.Services.Interfaces;
using NuGet.Common;

namespace ECJ_Compras.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDto login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _authService.Login(login.Usuario, login.Senha);
                    var token = _authService.GerarToken(usuario);

                    HttpContext.Response.Cookies.Append("jwtToken", new JwtSecurityTokenHandler().WriteToken(token), new CookieOptions
                    {
                        HttpOnly = true, // Garante que a cookie só seja acessível pelo servidor
                        Expires = DateTime.UtcNow.AddHours(24) // Define o tempo de expiração da cookie
                    });
                    HttpContext.Response.Cookies.Append("usuario", usuario.Nome);

                    return Redirect("Home/Index");
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("jwtToken");
            return Redirect("Index");
        }
    }
}
