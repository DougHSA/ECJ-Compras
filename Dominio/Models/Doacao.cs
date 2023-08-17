using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Doacao
{
    public int Id { get; set; }

    public DateTime Data { get; set; }

    public int IdProduto { get; set; }

    public int IdPessoa { get; set; }

    public virtual Pessoa IdPessoaNavigation { get; set; } = null!;

    public virtual Produto IdProdutoNavigation { get; set; } = null!;
}
