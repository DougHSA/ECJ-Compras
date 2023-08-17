using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Produto
    {
        [Key] 
        public int Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public double Quantidade { get; set; } = 0;
        public string Unidade { get; set; } = string.Empty;
        public double PontosPorQuantidade { get; set; }
        public int Pontos { get; set; }

        public ICollection<Doacao> Doacoes { get; set; }
    }
}
