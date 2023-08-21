using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Doacao
{
    public int Id { get; set; }

    public double Quantidade { get; set; }

    public DateTime Data { get; set; }

    public int IdProduto { get; set; }

    public int IdEquipe { get; set; }

    public virtual Equipe Equipe { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;
}
