using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Permissao
{
    public int id { get; set; }

    public string codigo { get; set; } = null!;

    public string? descricao { get; set; }

    public virtual ICollection<Perfil> perfil { get; set; } = new List<Perfil>();
}
