using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IDoacaoService
    {
        List<Doacao> BuscarDoacoes();
        Doacao InserirNovaDoacao(DoacaoDto doacao);
        Doacao DeletarDoacao(int id);
        string[] BuscarEquipes();
        string[] BuscarProdutos();
        string BuscarUnidade(string produto);
    }
}
