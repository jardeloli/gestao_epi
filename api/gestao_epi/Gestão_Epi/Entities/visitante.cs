using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Visitante
{
    public int id { get; set; }

    public string? nome { get; set; }

    public string? documento { get; set; }

    public virtual ICollection<Notificacao> notificacao { get; set; } = new List<Notificacao>();

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();
}
