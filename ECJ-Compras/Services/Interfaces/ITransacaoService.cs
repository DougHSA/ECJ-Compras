using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface ITransacaoService
    {
        List<Transacao> BuscarTransacoesEntrada(string nomeUsuario = null);
        void InserirNovaEntrada(TransacaoDto transacao, string nomeUsuario);
        void DeletarEntrada(int id);

        List<Transacao> BuscarTransacoesSaida(string nomeUsuario = null);
        void InserirNovaSaida(TransacaoDto transacao, string nomeUsuario);
        void DeletarSaida(int id);
    }
}
