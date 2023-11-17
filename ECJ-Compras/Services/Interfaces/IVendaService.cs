using Dominio.Models;
using ECJ_Compras.Dto;

namespace ECJ_Compras.Services.Interfaces
{
    public interface IVendaService
    {
        List<Venda> BuscarVendas();
        Venda InserirNovaVenda(VendaDto venda);
        Venda DeletarVenda(int id);
        string[] BuscarConsignados();
        string[] BuscarProdutos(string consignado);
        decimal BuscarPreco(string produto);
    }
}
