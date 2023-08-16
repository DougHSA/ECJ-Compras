using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Models;

public partial class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Senha { get; set; } = null!;

    public string Nome { get; set; } = null!;

    public string Nivel { get; set; } = null!;

    public ICollection<Transacao> Transacoes { get; set; }
}
