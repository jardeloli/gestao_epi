using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Notificacao
{
    public int id { get; set; }

    public string mensagem { get; set; } = null!;

    public int? colaborador_id { get; set; }

    public int? visitante_id { get; set; }

    public DateTime data_limite { get; set; }

    public int retirada_id { get; set; }

    public string status { get; set; } = null!;

    public virtual Colaborador? colaborador { get; set; }

    public virtual Retirada_devolucao retirada { get; set; } = null!;

    public virtual Visitante? visitante { get; set; }
}
