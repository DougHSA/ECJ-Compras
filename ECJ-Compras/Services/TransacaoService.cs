using Dominio.Context;
using Dominio.Models;
using ECJ_Compras.Dto;
using ECJ_Compras.Enums;
using ECJ_Compras.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace ECJ_Compras.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly EjcContext _context;

        public TransacaoService(EjcContext context)
        {
            _context = context;
        }

        public List<Transacao> BuscarTransacoesEntrada(string nomeUsuario = null)
        {
            var transacoes = _context.Transacoes.Include(u => u.Usuario).Where(t => t.Tipo == EnumTipoTransacao.Entrada.ToString()).ToList();
            if(nomeUsuario != null)
                transacoes = transacoes.Where(t=>t.Usuario.Nome == nomeUsuario).ToList();
            return transacoes;
        }

        public Transacao InserirNovaEntrada(TransacaoDto transacaoDto, string nomeUsuario)
        {
            var cliente = _context.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
            if (cliente == null)
                throw new Exception("Não foi possível realizar lançamento.");
            var valorDecimal = decimal.Parse(Regex.Match(transacaoDto.Valor, "([0-9]+)(,[0-9]{2})").Value);
            var transacao = new Transacao()
            {
                Data = DateTime.Now,
                Descricao = transacaoDto.Descricao,
                IdUsuario = cliente.Id,
                MetodoPagamento = transacaoDto.MetodoPagamento,
                Tipo = EnumTipoTransacao.Entrada.ToString(),
                Valor = valorDecimal
            };
            _context.Transacoes.Add(transacao);
            _context.SaveChanges();
            return transacao;
        }

        public Transacao DeletarEntrada(int id)
        {
            var transacao = _context.Transacoes.FirstOrDefault(t => t.Id == id);
            if (transacao == null)
                throw new Exception("");
            _context.Transacoes.Remove(transacao);
            _context.SaveChanges();
            return transacao;
        }

        public List<Transacao> BuscarTransacoesSaida(string nomeUsuario = null)
        {
            var transacoes = _context.Transacoes.Include(u => u.Usuario).Where(t => t.Tipo == EnumTipoTransacao.Saída.ToString()).ToList();
            if (nomeUsuario != null)
                transacoes = transacoes.Where(t => t.Usuario.Nome == nomeUsuario).ToList();
            return transacoes;
        }

        public Transacao InserirNovaSaida(TransacaoDto transacaoDto, string nomeUsuario)
        {
            var cliente = _context.Usuarios.FirstOrDefault(u => u.Nome == nomeUsuario);
            if (cliente == null)
                throw new Exception("Não foi possível realizar lançamento.");
            var valorDecimal = decimal.Parse(Regex.Match(transacaoDto.Valor, "([0-9]+)(,[0-9]{2})").Value);
            var transacao = new Transacao()
            {
                Data = DateTime.Now,
                Descricao = transacaoDto.Descricao,
                IdUsuario = cliente.Id,
                MetodoPagamento = transacaoDto.MetodoPagamento,
                Tipo = EnumTipoTransacao.Entrada.ToString(),
                Valor = valorDecimal
            };
            _context.Transacoes.Add(transacao);
            _context.SaveChanges();
            return transacao;
        }

        public Transacao DeletarSaida(int id)
        {
            var transacao = _context.Transacoes.FirstOrDefault(t => t.Id == id);
            if (transacao == null)
                throw new Exception("");
            _context.Transacoes.Remove(transacao);
            _context.SaveChanges();
            return transacao;
        }
    }
}
