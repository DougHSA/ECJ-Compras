using Dominio.Models;
using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class PesquisaDoacoesDto
    {
        public string? Equipe { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string? CategoriaProduto { get; set; }
        public string? Ordenar { get; set; }
        public bool OrdenarDecrescente { get; set; }
    }
}
