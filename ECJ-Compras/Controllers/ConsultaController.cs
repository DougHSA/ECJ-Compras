using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Emit;
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
        public IActionResult Lancamentos(PesquisaLancamentosDto lancamentosDto, int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaLancamentos = _consultaService.BuscarLancamentos(lancamentosDto);
            if (listaLancamentos.Any())
            {
                var lista = listaLancamentos.OrderByDescending(e => e.Data).Skip(((page ?? 1) - 1) * length).Take(length);
                ViewBag.ListaLancamentos = lista;
                ViewBag.TabelaCount = listaLancamentos.Count();
            }
            return View();
        }

        [Authorize]
        public IActionResult Produtos(PesquisaProdutosDto produtosDto, int? page = null, int length = 20)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaProdutos = _consultaService.BuscarProdutos(produtosDto);
            if (listaProdutos.Any())
            {
                var lista = listaProdutos.OrderByDescending(e => e.Quantidade).Skip(((page ?? 1) - 1) * length).Take(length);
                ViewBag.ListaProdutos = lista;
                ViewBag.TabelaCount = listaProdutos.Count();
            }
            return View();
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
        public IActionResult BuscarOrdenacaoDoacao()
        {
            string[] result = {"Equipe","Produto","Data"};
            Array.Sort(result);

            return Json(result);
        }

        public IActionResult BuscarTipos()
        {
            string[] result = typeof(EnumTipoTransacao).GetEnumNames();
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarMetodosPagamentos()
        {
            string[] result = typeof(EnumMetodoPagamento).GetEnumNames();
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarAutores()
        {
            string[] result = _consultaService.BuscarAutores();
            Array.Sort(result);
            return Json(result);
        }

        public IActionResult BuscarOrdenacaoLancamentos()
        {
            string[] result = { "Descrição", "Tipo", "Método de Pagamento", "Valor", "Data" };
            Array.Sort(result);

            return Json(result);
        }

        public IActionResult BuscarOrdenacaoProdutos()
        {
            string[] result = { "Produto", "Quantidade", "Unidade"};
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
