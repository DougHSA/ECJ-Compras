using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Controllers
{
    public class DoacaoController : BaseController
    {
        private readonly IDoacaoService _doacaoService;
        private readonly IEmailService _emailService;

        public DoacaoController(IDoacaoService doacaoService, IEmailService emailService)
        {
            _doacaoService = doacaoService;
            _emailService = emailService;
        }
        [Authorize]
        public IActionResult Index(int? page = null)
        {
            var listaDoacoes = _doacaoService.BuscarDoacoes();
            if (listaDoacoes.Any())
            {
                var lista = listaDoacoes.OrderByDescending(e => e.Data).Skip(((page ?? 1)-1)*5).Take(5);
                ViewBag.ListaDoacoes = lista;
                ViewBag.TabelaCount = listaDoacoes.Count();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult InserirDoacao(DoacaoDto doacaoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var doacao = _doacaoService.InserirNovaDoacao(doacaoDto);
                    _emailService.EnviarEmailNovaDoacao(doacao,VerificarUsuario());
                }
                else
                {
                    throw new Exception("Preencha todos os campos.");
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("/Doacao/DeletarDoacao/{id}")]
        [Authorize]
        public IActionResult DeletarDoacao(int id)
        {
            try
            {
                var doacao = _doacaoService.DeletarDoacao(id);
                _emailService.EnviarEmailDeletarDoacao(doacao, VerificarUsuario());
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível deletar a transação");
            }
        }

        public IActionResult BuscarEquipes()
        {
            string[] result = _doacaoService.BuscarEquipes();
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarNomes(string equipe)
        {
            string[] result = _doacaoService.BuscarNomes(equipe);
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarProdutos()
        {
            string[] result = _doacaoService.BuscarProdutos();
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarUnidade(string produto)
        {
            string result = _doacaoService.BuscarUnidade(produto);

            return Json(result);
        }
    }
}
