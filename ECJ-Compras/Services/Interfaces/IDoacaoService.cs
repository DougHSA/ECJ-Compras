using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IDoacaoService
    {
        List<Doacao> BuscarDoacoes();
        void InserirNovaDoacao(DoacaoDto doacao);
        void DeletarDoacao(int id);
        string[] BuscarEquipes();
        string[] BuscarNomes(string equipe);
        string[] BuscarProdutos();
        string BuscarUnidade(string produto);
    }
}
