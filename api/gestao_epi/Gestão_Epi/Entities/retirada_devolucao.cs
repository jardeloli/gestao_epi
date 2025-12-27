using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Retirada_devolucao
{
    public int id { get; set; }

    public int usuario_id { get; set; }

    public int colaborador_id { get; set; }

    public int visitante_id { get; set; }

    public int epi_id { get; set; }

    public DateTime data_retirada { get; set; }

    public DateTime? data_devolucao { get; set; }

    public string? justificativa_retirada { get; set; }

    public string? justificativa_devolucao { get; set; }

    public virtual Colaborador colaborador { get; set; } = null!;

    public virtual Epi epi { get; set; } = null!;

    public virtual Notificacao? notificacao { get; set; }

    public virtual Usuario usuario { get; set; } = null!;

    public virtual Visitante visitante { get; set; } = null!;
}
