using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Services
{
    public class DoacaoService : IDoacaoService
    {
        private readonly EjcContext _context;

        public DoacaoService(EjcContext context)
        {
            _context = context;
        }

        public List<Doacao> BuscarDoacoes()
        {
            var doacoes = _context.Doacoes?.Include(u => u.Equipe).Include(x=>x.Produto).ToList();
            return doacoes;
        }

        public Doacao InserirNovaDoacao(DoacaoDto doacaoDto)
        {
            var equipe = _context.Equipes.FirstOrDefault(p => p.Nome == doacaoDto.EquipePessoa);
            var produto = _context.Produtos.FirstOrDefault(p => p.Categoria == doacaoDto.CategoriaProduto);

            if (equipe == null || produto == null)
                throw new Exception("Não foi possível adicionar Doação");

            produto.Quantidade += doacaoDto.QuantidadeProduto;
            var doacao = new Doacao()
            {
                Data = DateTime.Now,
                IdEquipe = equipe.Id,
                IdProduto = produto.Id,
                Quantidade = doacaoDto.QuantidadeProduto
            };
            _context.Doacoes.Add(doacao);
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            var doacoes = _context.Doacoes.Include(p => p.Equipe).Include(p => p.Produto)
                            .Where(d => d.Equipe.Nome == equipe.Nome)
                            .GroupBy(p => p.Produto.Categoria)
                            .ToList();
            equipe.Pontos = 0;
            equipe.PontosAdicionais += (Math.Floor(doacaoDto.QuantidadeProduto / produto.QuantidadeMinimaParaPontos) * produto.Pontos) * (doacaoDto.FatorDeMultiplicacao - 1);
            foreach (var grupo in doacoes)
            {
                var qt = grupo.Sum(q => q.Quantidade);
                var pontosDoProduto = grupo.First().Produto.Pontos;
                var qtMinimaParaPontuar = grupo.First().Produto.QuantidadeMinimaParaPontos;
                int pontoNaCategoria = (int)Math.Floor(qt / qtMinimaParaPontuar) * pontosDoProduto;
                equipe.Pontos += pontoNaCategoria;
            }
            _context.Equipes.Update(equipe);
            _context.SaveChanges();
            return doacao;
        }

        public Doacao DeletarDoacao(int id)
        {
            var doacao = _context.Doacoes.FirstOrDefault(t => t.Id == id);
            var produto = _context.Produtos.FirstOrDefault(t=> t.Id == doacao.IdProduto);
            var equipe = _context.Equipes.FirstOrDefault(t => t.Id == doacao.IdEquipe);
            produto.Quantidade -= doacao.Quantidade;
            if (doacao == null)
                throw new Exception("");
            _context.Doacoes.Remove(doacao);
            _context.Produtos.Update(produto);
            _context.SaveChanges();

            var doacoes = _context.Doacoes.Include(p => p.Equipe).Include(p => p.Produto)
                            .Where(d => d.Equipe.Nome == equipe.Nome)
                            .GroupBy(p => p.Produto.Categoria)
                            .ToList();
            equipe.Pontos = 0;
            foreach (var grupo in doacoes)
            {
                var qt = grupo.Sum(q => q.Quantidade);
                var pontosDoProduto = grupo.First().Produto.Pontos;
                var qtMinimaParaPontuar = grupo.First().Produto.QuantidadeMinimaParaPontos;
                int pontoNaCategoria = (int)Math.Floor(qt / qtMinimaParaPontuar) * pontosDoProduto;
                equipe.Pontos += pontoNaCategoria;
            }
            _context.Equipes.Update(equipe); 
            _context.SaveChanges();
            return doacao;
        }

        public string[] BuscarEquipes()
        {
            string[] result = _context.Equipes.Select(p => p.Nome).ToArray();
            Array.Sort(result);
            return result;
        }

        public string[] BuscarProdutos()
        {
            string[] result = _context.Produtos.Select(p=>p.Categoria).ToArray();
            Array.Sort(result);
            return result;
        }

        public string BuscarUnidade(string produto)
        {
            string result = _context.Produtos.Where(p => p.Categoria == produto).Select(p=>p.Unidade).First();
            return result;
        }
    }
}
