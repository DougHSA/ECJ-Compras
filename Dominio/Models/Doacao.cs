using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Models;

public class Doacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime Data { get; set; }

    public int IdProduto { get; set; }

    public int IdPessoa { get; set; }

    public virtual Produto Produto { get; set; }

    public virtual Pessoa Pessoa { get; set; }
}
