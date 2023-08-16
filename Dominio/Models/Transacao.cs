using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models;

public partial class Transacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public decimal Valor { get; set; }

    public DateTime Data { get; set; }

    public string MetodoPagamento { get; set; } = null!;

    public int IdUsuario { get; set; }

    public virtual Usuario Usuario { get; set; }
}
