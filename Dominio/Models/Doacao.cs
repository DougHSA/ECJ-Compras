using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Doacao
{
    public int Id { get; set; }

    public double Quantidade { get; set; }

    public DateTime Data { get; set; }

    public int IdProduto { get; set; }

    public int IdPessoa { get; set; }

    public virtual Pessoa Pessoa { get; set; } = null!;

    public virtual Produto Produto { get; set; } = null!;
}
