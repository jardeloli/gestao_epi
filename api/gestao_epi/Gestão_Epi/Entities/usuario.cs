using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Gestão_Epi.Entities;

public partial class Usuario
{
    public int id { get; set; }

    public string? nome { get; set; }

    public string? email { get; set; }

    public byte[] senhaHash { get; private set; } = null!;
    public byte[] senhaSalt { get; private set; } = null!;

    public int perfil_id { get; set; }

    public virtual Perfil perfil { get; set; } = null!;

    public virtual ICollection<Retirada_devolucao> retirada_devolucao { get; set; } = new List<Retirada_devolucao>();

    public void Defsenha(string senha)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        senhaSalt = hmac.Key;
        senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
    }

    public bool Verifsenha(string senha)
    {
        using var hmac = new HMACSHA512(senhaSalt);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));
        return hash.SequenceEqual(senhaHash);
    }
}
