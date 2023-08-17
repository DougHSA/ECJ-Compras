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
    public class LancamentoController : BaseController
    {
        private readonly ITransacaoService _transacaoService;
        private readonly IEmailService _emailService;

        public LancamentoController(ITransacaoService transacaoService, IEmailService emailService)
        {
            _transacaoService = transacaoService;
            _emailService = emailService;
        }
        #region Entrada
        [Authorize]
        public IActionResult Entrada(int? page = null)
        {
            var listaEntrada = _transacaoService.BuscarTransacoesEntrada(VerificarUsuario());
            if (listaEntrada.Any())
            {
                var lista = listaEntrada.OrderByDescending(e => e.Data).Skip(((page ?? 1)-1)*5).Take(5);
                ViewBag.ListaEntrada = lista;
                ViewBag.TabelaCount = listaEntrada.Count();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult InserirEntrada(TransacaoDto transacaoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var transacao = _transacaoService.InserirNovaEntrada(transacaoDto, VerificarUsuario());
                    _emailService.EnviarEmailNovaTransacao(transacao);
                }
                else
                {
                    throw new Exception("Preencha todos os campos.");
                }
                return RedirectToAction("Entrada");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("/Lancamento/DeletarEntrada/{id}")]
        [Authorize]
        public IActionResult DeletarEntrada(int id)
        {
            try
            {
                var transacao = _transacaoService.DeletarEntrada(id);
                _emailService.EnviarEmailDeletarTransacao(transacao);
                return RedirectToAction("Entrada");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível deletar a transação");
            }
        }


        public IActionResult Saida()
        {
            var listaSaida = _transacaoService.BuscarTransacoesSaida(VerificarUsuario());
            if (listaSaida.Any())
                ViewBag.ListaSaida = listaSaida.OrderByDescending(e => e.Data).Take(5);
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult InserirSaida(TransacaoDto transacaoDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var transacao = _transacaoService.InserirNovaSaida(transacaoDto, VerificarUsuario());
                    _emailService.EnviarEmailNovaTransacao(transacao);
                }
                else
                {
                    throw new Exception("Preencha todos os campos.");
                }
                return RedirectToAction("Saida");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("/Lancamento/DeletarSaida/{id}")]
        [Authorize]
        public IActionResult DeletarSaida(int id)
        {
            try
            {
                var transacao = _transacaoService.DeletarSaida(id);
                _emailService.EnviarEmailDeletarTransacao(transacao);
                return RedirectToAction("Saida");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível deletar a transação");
            }
        }

        public IActionResult BuscarMetodosDePagamento()
        {
            string[] result = Enum.GetNames(typeof(EnumMetodoPagamento));
            Array.Sort(result);

            return Json(result);
        }
        #endregion
    }
}
