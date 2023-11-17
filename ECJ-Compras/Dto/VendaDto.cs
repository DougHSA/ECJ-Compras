using Dominio.Models;
using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class VendaDto
    {
        public string Consignado { get; set; } = string.Empty;
        public string DescricaoProduto { get; set; } = string.Empty;
        public double Quantidade { get; set; } = 0;
        public double PrecoProduto { get; set; } = 0;
    }
}
