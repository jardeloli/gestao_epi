using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Epi
{
<<<<<<< HEAD
   
=======
    internal object descricao;
>>>>>>> 88bb9720af660b61810a8b79b7b1d84c2b8276eb

    public int id { get; set; }

    public string? nome { get; set; }

    public int? ca { get; set; }

    public string? tamanho { get; set; }
<<<<<<< HEAD

    public string? descricao { get; set; }
=======
>>>>>>> 88bb9720af660b61810a8b79b7b1d84c2b8276eb
    public DateOnly? validade { get; set; }

    public virtual Estoque? estoque { get; set; }

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();
}
