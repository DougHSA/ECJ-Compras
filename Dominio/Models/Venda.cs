using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Venda
{
    public int Id { get; set; }

    public int IdProdutoVenda { get; set; }

    //public int IdUsuario { get; set; }

    public double Quantidade { get; set; }

    public DateTime Data { get; set; }

    //public virtual Usuario Usuario { get; set; } = null!;

    public virtual ProdutoVenda ProdutoVenda { get; set; } = null!;
}
