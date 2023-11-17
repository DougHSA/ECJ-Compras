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
    public class VendaController : BaseController
    {
        private readonly IVendaService _vendaService;

        public VendaController(IVendaService vendaService)
        {
            _vendaService = vendaService;
        }
        [Authorize]
        public IActionResult Index(int? page = null)
        {
            string role = VerificarRole();
            ViewBag.Autorizacao = role;

            var listaVendas = _vendaService.BuscarVendas();
            var listaVendas = new List<Venda>();
            if (listaVendas.Any())
            {
                var lista = listaVendas.OrderByDescending(e => e.Data).Skip(((page ?? 1)-1)*5).Take(5);
                ViewBag.ListaVendas = lista;
                ViewBag.TabelaCount = listaVendas.Count();
            }
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult InserirVenda(VendaDto vendaDto)
        {
            try
            {
                //var venda = _vendaService.InserirNovaVenda(vendaDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/Venda/DeletarVenda/{id}")]
        [Authorize]
        public IActionResult DeletarVenda(int id)
        {
            try
            {
                var venda = _vendaService.DeletarVenda(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                ViewBag.ErrorMessage = TempData["ErrorMessage"] as string;
            }
             return RedirectToAction("Index");
        }

        public IActionResult BuscarConsignados()
        {
            //string[] result = _vendaService.BuscarConsignados();
            string[] result = { "Consignado 1", "Consignado 2", "Consignado 3", "EJC" };
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarProdutos()
        {
            //string[] result = _vendaService.BuscarProdutos();
            string[] result = { "Produto 1", "Produto 2", "Produto 3", "Produto 4" };
            Array.Sort(result);

            return Json(result);
        }
        public IActionResult BuscarPreco(string produto)
        {
            if(produto == null)
                return Json(null);
            //decimal result = _vendaService.BuscarPreco(produto);
            decimal result = 10.00m;
            if (produto.Contains("2"))
                result = 20.00m;
            if (produto.Contains("3"))
                result = 30.00m;
            return Json(result);
        }
    }
}
