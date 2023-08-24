using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IConsultaService
    {
        List<Doacao> BuscarDoacoes(PesquisaDoacoesDto doacaoDto);
        string[] BuscarEquipes();
        List<Equipe> BuscarPontos();
        string[] BuscarProdutos();
    }
}
