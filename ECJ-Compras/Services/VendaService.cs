using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Services
{
    public class VendaService : IVendaService
    {
        private readonly EjcContext _context;

        public VendaService(EjcContext context)
        {
            _context = context;
        }

        public List<Venda> BuscarVendas()
        {
            var vendas = _context.Vendas?.Include(u => u.ProdutoVenda).ToList();
            return vendas;
        }

        public Venda InserirNovaVenda(VendaDto vendaDto)
        {
            var produto = _context.ProdutosVenda.First(p => p.Descricao == vendaDto.DescricaoProduto);
            var venda = new Venda()
            {
                Data = DateTime.Now,
                Quantidade = vendaDto.Quantidade,
                IdProdutoVenda = produto.Id
            };
            produto.QuantidadeAtual += vendaDto.Quantidade;
            _context.Vendas?.Add(venda);
            _context.ProdutosVenda?.Update(produto);
            _context.SaveChanges();
            return venda;
        }

        public Venda DeletarVenda(int id)
        {
            var venda =  _context.Vendas?.Include(v=>v.ProdutoVenda).Where(u => u.Id == id).SingleOrDefault();
            if (venda == null)
                return null;
            venda.ProdutoVenda.QuantidadeAtual -= venda.Quantidade;
            _context.Vendas?.Remove(venda);
            _context.ProdutosVenda?.Update(venda.ProdutoVenda);
            _context.SaveChanges();
            return venda;
        }

        public string[] BuscarConsignados()
        {
            string[] result = _context.ProdutosVenda.Select(p => p.Consignado).ToArray();
            Array.Sort(result);
            return result;
        }

        public string[] BuscarProdutos(string consignado)
        {
            string[] result = _context.ProdutosVenda.Select(p=>p.Consignado).Where(p=> p == consignado).ToArray();
            Array.Sort(result);
            return result;
        }

        public decimal BuscarPreco(string produto)
        {
            decimal result = _context.ProdutosVenda.Where(p => p.Descricao == produto).Select(p=>p.Preco).First();
            return result;
        }
    }
}
