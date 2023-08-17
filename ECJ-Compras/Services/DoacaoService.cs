using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
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
            var doacoes = _context.Doacoes.Include(u => u.Pessoa).Include(x=>x.Produto).ToList();
            return doacoes;
        }

        public void InserirNovaDoacao(DoacaoDto doacaoDto)
        {
            var cliente = _context.Usuarios.FirstOrDefault();
            if (cliente == null)
                throw new Exception("Não foi possível realizar lançamento.");
            var doacao = new Doacao()
            {
                Data = DateTime.Now,
            };
            _context.Doacoes.Add(doacao);
            _context.SaveChanges();
        }

        public void DeletarDoacao(int id)
        {
            var doacao = _context.Transacoes.FirstOrDefault(t => t.Id == id);
            if (doacao == null)
                throw new Exception("");
            _context.Transacoes.Remove(doacao);
            _context.SaveChanges();
        }
    }
}
