using Gestão_Epi.Models.Pessoa_Model;

namespace Gestão_Epi.Models
{
    public class VisitanteRequest : PessoaRequest
    {
        public string documento { get; set; }
    }
}
