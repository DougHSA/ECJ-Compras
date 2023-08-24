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
    public class ConsultaService : IConsultaService
    {
        private readonly EjcContext _context;

        public ConsultaService(EjcContext context)
        {
            _context = context;
        }

        public List<Doacao> BuscarDoacoes(PesquisaDoacoesDto doacaoDto)
        {
            var doacoes = _context.Doacoes.Include(d=>d.Equipe).Include(d=>d.Produto).AsQueryable();
            if (!string.IsNullOrEmpty(doacaoDto.CategoriaProduto))
            {
                doacoes = doacoes.Where(d => d.Produto.Categoria.Contains(doacaoDto.CategoriaProduto));
            }
            if (doacaoDto.DataInicio.HasValue && doacaoDto.DataFim.HasValue)
            {
                doacoes = doacoes.Where(d => d.Data >= doacaoDto.DataInicio && d.Data<= doacaoDto.DataFim.Value.AddDays(1));
            }
            if (!string.IsNullOrEmpty(doacaoDto.Equipe))
            {
                doacoes = doacoes.Where(d => d.Equipe.Nome.Contains(doacaoDto.Equipe));
            }

            List<Doacao> result = doacoes.ToList();
            return result;
        }

        public string[] BuscarEquipes()
        {
            string[] result = _context.Equipes.Select(p => p.Nome).ToArray();
            Array.Sort(result);
            return result;
        }

        public string[] BuscarProdutos()
        {
            string[] result = _context.Produtos.Select(p => p.Categoria).ToArray();
            Array.Sort(result);
            return result;
        }

        public List<Equipe> BuscarPontos()
        {
            List<Equipe> result = _context.Equipes.ToList();
            return result;
        }

    }
}
