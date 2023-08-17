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
            var doacoes = _context.Doacoes?.Include(u => u.Pessoa).Include(x=>x.Produto).ToList();
            return doacoes;
        }

        public Doacao InserirNovaDoacao(DoacaoDto doacaoDto)
        {
            var pessoa = _context.Pessoas.FirstOrDefault(p => p.Nome == doacaoDto.NomePessoa && p.Equipe == doacaoDto.EquipePessoa);
            var produto = _context.Produtos.FirstOrDefault(p => p.Categoria == doacaoDto.CategoriaProduto);

            if (pessoa == null || produto == null)
                throw new Exception("Não foi possível adicionar Doação");

            produto.Quantidade += doacaoDto.QuantidadeProduto;

            var doacao = new Doacao()
            {
                Data = DateTime.Now,
                IdPessoa = pessoa.Id,
                IdProduto = produto.Id,
                Quantidade = doacaoDto.QuantidadeProduto
            };
            _context.Doacoes.Add(doacao);
            _context.Produtos.Update(produto);
            _context.SaveChanges();
            var doacoes = _context.Doacoes.Include(p => p.Pessoa).Include(p => p.Produto)
                            .Where(d => d.Pessoa.Nome == pessoa.Nome && d.Pessoa.Equipe == pessoa.Equipe)
                            .GroupBy(p => p.Produto.Categoria)
                            .ToList();
            pessoa.Pontos = 0;
            foreach (var grupo in doacoes)
            {
                var qt = grupo.Sum(q => q.Quantidade);
                var pontosDoProduto = grupo.First().Produto.Pontos;
                var qtMinimaParaPontuar = grupo.First().Produto.QuantidadeMinimaParaPontos;
                int pontoNaCategoria = (int)Math.Floor(qt / qtMinimaParaPontuar) * pontosDoProduto;
                pessoa.Pontos += pontoNaCategoria;
            }
            _context.Pessoas.Update(pessoa);
            _context.SaveChanges();
            return doacao;
        }

        public Doacao DeletarDoacao(int id)
        {
            var doacao = _context.Doacoes.FirstOrDefault(t => t.Id == id);
            var produto = _context.Produtos.FirstOrDefault(t=> t.Id == doacao.IdProduto);
            var pessoa = _context.Pessoas.FirstOrDefault(t => t.Id == doacao.IdPessoa);
            produto.Quantidade -= doacao.Quantidade;
            if (doacao == null)
                throw new Exception("");
            _context.Doacoes.Remove(doacao);
            _context.Produtos.Update(produto);
            _context.SaveChanges();

            var doacoes = _context.Doacoes.Include(p => p.Pessoa).Include(p => p.Produto)
                            .Where(d => d.Pessoa.Nome == pessoa.Nome && d.Pessoa.Equipe == pessoa.Equipe)
                            .GroupBy(p => p.Produto.Categoria)
                            .ToList();
            pessoa.Pontos = 0;
            foreach (var grupo in doacoes)
            {
                var qt = grupo.Sum(q => q.Quantidade);
                var pontosDoProduto = grupo.First().Produto.Pontos;
                var qtMinimaParaPontuar = grupo.First().Produto.QuantidadeMinimaParaPontos;
                int pontoNaCategoria = (int)Math.Floor(qt / qtMinimaParaPontuar) * pontosDoProduto;
                pessoa.Pontos += pontoNaCategoria;
            }
            _context.Pessoas.Update(pessoa); 
            _context.SaveChanges();
            return doacao;
        }

        public string[] BuscarEquipes()
        {
            string[] result = { "Compras", "Sala","Lt. Interna","Lt. Externa" };
            return result;
        }

        public string[] BuscarNomes(string equipe)
        {
            string[] result = { "Douglas Andrade", "Amanda", "Italo"};
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
