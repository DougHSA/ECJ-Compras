using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Produto
{
    public int Id { get; set; }

    public string Categoria { get; set; } = null!;

    public double Quantidade { get; set; }

    public string? Unidade { get; set; }

    public double QuantidadeMinimaParaPontos { get; set; }

    public int Pontos { get; set; }

    public virtual ICollection<Doacao> Doacoes { get; set; } = new List<Doacao>();
}
