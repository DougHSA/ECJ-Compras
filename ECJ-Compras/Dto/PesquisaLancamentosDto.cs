using Dominio.Models;
using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class PesquisaLancamentosDto
    {
        public string EquipePessoa { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public string CategoriaProduto { get; set; } = string.Empty;
        public double QuantidadeProduto { get; set; } = 0;
        public string UnidadeProduto { get; set; } = string.Empty;
    }
}
