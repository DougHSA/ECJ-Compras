using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IConsultaService
    {
        List<Doacao> BuscarDoacoes(PesquisaDoacoesDto doacaoDto);
        List<Transacao> BuscarLancamentos(PesquisaLancamentosDto lancamentosDto);
        List<Produto> BuscarProdutos(PesquisaProdutosDto produtosDto);
        string[] BuscarAutores();
        string[] BuscarEquipes();
        List<Equipe> BuscarPontos();
        string[] BuscarProdutos();
    }
}
