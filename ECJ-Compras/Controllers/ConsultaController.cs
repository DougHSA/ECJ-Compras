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
    public class ConsultaController : BaseController
    {
        private readonly IDoacaoService _doacaoService;
        private readonly ITransacaoService _transacaoService;

        public ConsultaController(IDoacaoService doacaoService, ITransacaoService transacaoService)
        {
            _doacaoService = doacaoService;
            _transacaoService = transacaoService;
        }
        [Authorize]
        public IActionResult Lancamentos(int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaDoacoes = _doacaoService.BuscarDoacoes();
            if (listaDoacoes.Any())
            {
                var lista = listaDoacoes.OrderByDescending(e => e.Data).Skip(((page ?? 1)-1)* length).Take(length);
                ViewBag.ListaDoacoes = lista;
                ViewBag.TabelaCount = listaDoacoes.Count();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult PesquisaLancamento(DoacaoDto doacaoDto)
        {
            try
            {
                var doacao = _doacaoService.InserirNovaDoacao(doacaoDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Doacoes(int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaDoacoes = _doacaoService.BuscarDoacoes();
            if (listaDoacoes.Any())
            {
                var lista = listaDoacoes.OrderByDescending(e => e.Data).Skip(((page ?? 1) - 1) * length).Take(length);
                ViewBag.ListaDoacoes = lista;
                ViewBag.TabelaCount = listaDoacoes.Count();
            }
            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult PesquisaDoacao(DoacaoDto doacaoDto)
        {
            try
            {
                var doacao = _doacaoService.InserirNovaDoacao(doacaoDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Pontuacao()
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var equipes = _doacaoService.BuscarPontos();
            if (equipes.Any())
            {
                ViewBag.ListaDoacoes = equipes.OrderByDescending(p=>p.Pontos);
            }
            return View();
        }
    }
}
