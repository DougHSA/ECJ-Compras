﻿using System;
using System.Collections.Generic;

namespace Dominio.Models;

public partial class Equipe
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int Pontos { get; set; }

    public virtual ICollection<Doacao> Doacoes { get; set; } = new List<Doacao>();
}