using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECJ_Compras.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ITransacaoService _transacaoService;

        public HomeController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }

        [Authorize]
        public IActionResult Index()
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaEntrada = _transacaoService.BuscarTransacoesEntrada();
            var listaSaida = _transacaoService.BuscarTransacoesSaida();

            var listaEntradaTabela = listaEntrada.OrderByDescending(e => e.Data).Take(5);
            var listaSaidaTabela = listaSaida.OrderByDescending(s => s.Data).Take(5);

            var totalEntrada = _transacaoService.BuscarTransacoesEntrada().Sum(e=>e.Valor);
            var totalSaida = _transacaoService.BuscarTransacoesSaida().Sum(s=>s.Valor);

            var totalEntradaTabela = listaEntradaTabela.ToList().Sum(e=>e.Valor);
            var totalSaidaTabela = listaSaidaTabela.ToList().Sum(s=>s.Valor);

            var saldoGeral = totalEntrada - totalSaida;
            var saldoGeralTabela = totalEntradaTabela - totalSaidaTabela;


            ViewBag.ListaEntrada = listaEntradaTabela;
            ViewBag.ListaSaida = listaSaidaTabela;

            ViewBag.TotalEntrada = string.Format("{0:C}", totalEntrada);
            ViewBag.TotalSaida = string.Format("{0:C}", totalSaida);

            ViewBag.TotalEntradaTabela = string.Format("{0:C}", totalEntradaTabela);
            ViewBag.TotalSaidaTabela = string.Format("{0:C}", totalSaidaTabela);

            ViewBag.SaldoGeral = string.Format("{0:C}", saldoGeral);
            ViewBag.SaldoGeralTabela = string.Format("{0:C}", saldoGeralTabela);

            return View();
        }
        
    }
}
