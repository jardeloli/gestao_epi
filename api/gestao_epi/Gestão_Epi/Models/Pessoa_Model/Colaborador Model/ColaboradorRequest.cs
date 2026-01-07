namespace Gestão_Epi.Models.Pessoa_Model
{
    public class ColaboradorRequest : PessoaRequest
    {
        
        public string ? setor { get; set; }
        public string matricula { get; set; }

        public string ? novo_setor { get; set; }

        public string ? nova_matricula { get; set; }
    }
}
