namespace Gestão_Epi.Models
{
    public class PerfilResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<string> Permissoes { get; set; }
    }

}

