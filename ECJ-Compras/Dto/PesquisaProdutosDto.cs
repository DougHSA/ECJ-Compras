using Dominio.Models;
using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class PesquisaProdutosDto
    {
        public string Categoria { get; set; } = null!;

        public string? Quantidade { get; set; }

        public string? Unidade { get; set; }

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string? Ordenar { get; set; }
        public bool OrdenarDecrescente { get; set; }
    }
}
