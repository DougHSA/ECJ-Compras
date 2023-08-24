using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Controllers
{
    public class ConsultaController : BaseController
    {
        private readonly IConsultaService _consultaService;

        public ConsultaController(IConsultaService consultaService)
        {
            _consultaService = consultaService;
        }

        [Authorize]
        public IActionResult Lancamentos(int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            //var listaDoacoes = _consultaService.BuscarDoacoes();
            //if (listaDoacoes.Any())
            //{
            //    var lista = listaDoacoes.OrderByDescending(e => e.Data).Skip(((page ?? 1)-1)* length).Take(length);
            //    ViewBag.ListaDoacoes = lista;
            //    ViewBag.TabelaCount = listaDoacoes.Count();
            //}
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult PesquisaLancamento(DoacaoDto doacaoDto)
        {
            try
            {
                //var doacao = _consultaService.InserirNovaDoacao(doacaoDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Doacoes(PesquisaDoacoesDto doacaoDto,int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaDoacoes = _consultaService.BuscarDoacoes(doacaoDto);
            if (listaDoacoes.Any())
            {
                var lista = listaDoacoes.OrderByDescending(e => e.Data).Skip(((page ?? 1) - 1) * length).Take(length);
                ViewBag.ListaDoacoes = lista;
                ViewBag.TabelaCount = listaDoacoes.Count();
            }
            return View();
        }

        public IActionResult BuscarEquipes()
        {
            string[] result = _consultaService.BuscarEquipes();
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarProdutos()
        {
            string[] result = _consultaService.BuscarProdutos();
            Array.Sort(result);

            return Json(result);
        }

        [Authorize]
        public IActionResult Pontuacao()
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var equipes = _consultaService.BuscarPontos();
            if (equipes.Any())
            {
                ViewBag.ListaEquipes = equipes.OrderByDescending(p=>p.Pontos);
            }
            return View();
        }
    }
}
