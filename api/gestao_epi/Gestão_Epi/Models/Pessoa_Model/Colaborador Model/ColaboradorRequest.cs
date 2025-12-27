namespace Gestão_Epi.Models
{
    public class ColaboradorRequest: PessoaRequest
    {
        public string ? setor { get; set; }
        public int matricula { get; set; }
    }
}
