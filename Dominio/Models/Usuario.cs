using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Nivel { get; set; } = null!;

    public virtual ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    //public virtual ICollection<Venda> Vendas { get; set; } = new List<Venda>();
}
