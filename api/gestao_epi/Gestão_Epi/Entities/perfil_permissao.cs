namespace Gestão_Epi.Entities
{
    public class Perfil_Permissao
    {
        public int perfil_id { get; set; }
        public Perfil perfil { get; set; } = null!;

        public int permissao_id { get; set; }
        public Permissao permissao { get; set; } = null!;
    }
}
