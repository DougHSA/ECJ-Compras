using Dominio.Models;
using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class PesquisaLancamentosDto
    {
        public string Descricao { get; set; } = null!;

        public string Tipo { get; set; } = null!;

        public DateTime? DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public string MetodoPagamento { get; set; } = null!;

        public string? Autor { get; set; }
        public string? Ordenar { get; set; }
        public bool OrdenarDecrescente { get; set; }
    }
}
