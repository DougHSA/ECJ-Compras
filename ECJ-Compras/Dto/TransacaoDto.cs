using System.ComponentModel.DataAnnotations;

namespace ECJ_Compras.Dto
{
    public class TransacaoDto
    {
        public string Descricao { get; set; } = null!;

        public string Valor { get; set; }

        public string MetodoPagamento { get; set; } = null!;
    }
}
