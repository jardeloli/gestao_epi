using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Epi
{
    internal object descricao;

    public int id { get; set; }

    public string? nome { get; set; }

    public int? ca { get; set; }

    public string? tamanho { get; set; }
    public DateOnly? validade { get; set; }

    public virtual Estoque? estoque { get; set; }

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();
}
