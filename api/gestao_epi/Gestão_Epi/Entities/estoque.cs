using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Estoque
{
    public int id { get; set; }

    public int epi_id { get; set; }

    public int quantidade { get; set; }

    public virtual Epi epi { get; set; } = null!;
}
