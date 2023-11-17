using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class ProdutoVenda
{
    public int Id { get; set; }

    public string Consignado { get; set; }

    public string Descricao { get; set; }

    public double QuantidadeDisponibilizada { get; set; }

    public double QuantidadeAtual { get; set; }

    public decimal Preco { get; set; }

    public virtual ICollection<Venda> Vendas { get; set; } = new List<Venda>();
}
