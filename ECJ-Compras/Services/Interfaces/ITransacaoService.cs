using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface ITransacaoService
    {
        List<Transacao> BuscarTransacoesEntrada(string nomeUsuario = null);
        Transacao InserirNovaEntrada(TransacaoDto transacao, string nomeUsuario);
        Transacao DeletarEntrada(int id);

        List<Transacao> BuscarTransacoesSaida(string nomeUsuario = null);
        Transacao InserirNovaSaida(TransacaoDto transacao, string nomeUsuario);
        Transacao DeletarSaida(int id);
    }
}
