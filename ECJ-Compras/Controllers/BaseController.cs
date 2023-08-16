using Microsoft.AspNetCore.Mvc;

namespace ECJ_Compras.Controllers
{
    public class BaseController : Controller
    {
        public string VerificarUsuario()
        {
            var usuario = HttpContext.Request.Cookies["usuario"];
            return usuario;
        }
    }
}
