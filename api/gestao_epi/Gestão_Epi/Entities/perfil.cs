using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Perfil
{
    public int id { get; set; }

    public string nome { get; set; } = null!;

    public string descricao { get; set; } = null!;

    public virtual ICollection<Usuario> usuario { get; set; } = new List<Usuario>();

    public virtual ICollection<Permissao> permissao { get; set; } = new List<Permissao>();
}
