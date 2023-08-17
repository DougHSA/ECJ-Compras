using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models
{
    public class Pessoa
    {
        [Key] 
        public int Id { get; set; }
        public string Nome { get; set;} = string.Empty;
        public string Equipe { get; set; } = string.Empty;
        public int Pontos { get; set; } = 0;

        public ICollection<Doacao> Doacoes { get; set; }
    }
}
