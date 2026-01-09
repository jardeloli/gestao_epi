using System;
using System.Collections.Generic;

namespace Gestão_Epi.Entities;

public partial class Visitante
{
    public int id { get; set; }

    public string nome { get; set; }

    public string documento { get; private set; }

    public virtual ICollection<Notificacao> notificacao { get; set; } = new List<Notificacao>();

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();

    protected Visitante() { }
    public Visitante(string nome, string documento)
    {
        this.nome = nome;
        Definir_cpf(documento);
    }

    public void Atualizar_Dados(string nome, string documento)
    {
        this.nome = nome;
        Definir_cpf(documento);
    }
    private void Definir_cpf(string documento)
    {
       
        this.documento = documento;
    }


    
}
