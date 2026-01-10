using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Epi
{

    public int id { get; set; }

    public string? nome { get; set; }

    public int? ca { get; set; }

    public string? tamanho { get; set; }


    public string? descricao { get; set; }

    public DateOnly? validade { get; set; }

    public virtual Estoque? estoque { get; set; }

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();

    public static bool ChecarTamanhoCa(int ca)
    {
      
        return ca.ToString().Length >= 5;
    }

  
}
